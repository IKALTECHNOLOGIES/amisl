using MySql.Data.Entity;
using ONG.Entities;
using ONG.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONG.Utilitarios
{
    /// <summary>
    /// Summary description for TreeDataNivel1
    /// </summary>
    /// context.Request.Params.Get ("parent")
    public class TreeDataNivel1 : IHttpHandler
    {
        private List<Models.nivel> Records;


        public object Create(HttpContext httpCont)
        {
            amislEntities context = new amislEntities();
            try
            {
                nivel n = new nivel
                {
                    UUID = httpCont.Request.Params.Get ("id").ToString(),
                    NIVEL1 = getCodigo(httpCont.Request.Params.Get("parent").ToString()),
                    IDENTIFICADOR = httpCont.Request.Params.Get("text").ToString(),
                    SUPERIOR = httpCont.Request.Params.Get("parent").ToString()
                };
                context.nivel.Add(n);
                context.SaveChanges();
                return new { Result = "OK", Record = n };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        public string getCodigo(string UUID )
        {
            String codigo = "";
            amislEntities context = new amislEntities();
            int puntoPos=0;
            int num=0;
            int actual=0;
            try
            {
                var lista = context.nivel.Where(i => i.SUPERIOR == UUID);
                if (lista.Count() == 0) {
                    if (UUID != "root") { 
                        nivel n = context.nivel.First(i => i.UUID == UUID);
                        codigo = n.NIVEL1 + ".";
                    }
                    return codigo + "1";
                }
                foreach (Models.nivel niv in lista)
                {
                    if (codigo == "")
                    {
                        puntoPos = niv.NIVEL1.ToString().LastIndexOf('.');
                        if (puntoPos < 0) {
                            puntoPos = 0;
                            if (UUID != "root")
                            {
                                nivel n = context.nivel.First(i => i.UUID == UUID);
                                codigo = n.NIVEL1 + ".";
                            }
                            else
                            {
                                codigo = " ";
                            }
                        }else{
                                codigo = niv.NIVEL1.ToString().Substring(0, puntoPos+1);
                        }
                    }

                    if (puntoPos == 0){
                        num = Int32.Parse(niv.NIVEL1.ToString());
                    }
                    else {
                        num = Int32.Parse(niv.NIVEL1.ToString().Substring(puntoPos+1));
                    }

                    if (actual < num)
                    {
                        actual = num;
                    }
                }
                
            }
            catch (Exception ex)
            {
                return "1";
            }

            return codigo.Trim() + (actual+1);
        }

        public void ProcessRequest(HttpContext context)
        {
            string json="{}";
            if (context.Request.Params.Get("op") == "V")
            {
                json = GetTreedata();
            }else if(context.Request.Params.Get("op") == "C")
            {
                json = Create(context).ToString ();
            }
            else if (context.Request.Params.Get("op") == "B")
            {
                json = removeR(context).ToString();
            }
            else if (context.Request.Params.Get("op") == "M")
            {
                json = Edit(context).ToString();
            }
            
            context.Response.ContentType = "text/json";
            context.Response.Write(json);
        }


        public static object Delete(String UUID)
        {
            amislEntities context = new amislEntities();
            try
            {
                nivel n = context.nivel.First(i => i.UUID == UUID);
                context.nivel.Remove(n);
                context.SaveChanges();
                return new { Result = "OK", Record = UUID };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        public  object removeR(HttpContext ctx)
        {   
            borrarHijos(ctx.Request.Params.Get("UUID").ToString());
            return new { Result = "OK", Record = ctx.Request.Params.Get("UUID").ToString() };
        }

        public object  borrarHijos(String UUID) {

            amislEntities context = new amislEntities();
            try
            {   
                var lista = context.nivel.Where(i => i.SUPERIOR  == UUID);
                Delete(UUID);
                foreach (Models.nivel niv in lista)
                {
                    Delete(niv.UUID);
                    borrarHijos(niv.UUID);
                }
                return new { Result = "OK", Record = UUID };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }

        }

        public static object Edit(HttpContext httpCont)
        {
            amislEntities context = new amislEntities();
            try
            {
                nivel n2 = new nivel
                {
                    UUID = httpCont.Request.Params.Get("id").ToString(),
                };
                nivel n = context.nivel.First(i => i.UUID == n2.UUID );
                
                n.IDENTIFICADOR = httpCont.Request.Params.Get("text").ToString();
                context.SaveChanges();
                
                return new { Result = "OK", Record = n };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string GetTreedata()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.AddRange(new System.Data.DataColumn[] {
                new System.Data.DataColumn("Id"),
                new System.Data.DataColumn("Text"),
                new System.Data.DataColumn("ParentId")
             
        });


            ONG.Models.amislEntities context = new ONG.Models.amislEntities();
            try
            {

                var lista = from g in context.nivel select g ;
                int total = lista.Count();
                Records = lista.OrderBy(x => x.NIVEL1).ToList();

                foreach (Models.nivel niv in lista)
                {
                    if (niv.SUPERIOR == "root")
                    {
                        dt.Rows.Add(niv.UUID, niv.IDENTIFICADOR , 0);
                    }
                    else {
                        dt.Rows.Add(niv.UUID, niv.IDENTIFICADOR , niv.SUPERIOR );
                    }
                }

            }
            catch (Exception ex)
            {
                
            }

            
            Node root = new Node { id = "root", children = { }, text = "Root" };
            System.Data.DataView view = new System.Data.DataView(dt);
            view.RowFilter = "ParentId='0'";
            foreach (System.Data.DataRowView kvp in view)
            {
                string parentId = kvp["Id"].ToString();
                Node node = new Node { id = kvp["Id"].ToString(), text = kvp["text"].ToString() };
                root.children.Add(node);
                AddChildItems(dt, node, parentId);

            }
            return (new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(root));
        }

        private void AddChildItems(System.Data.DataTable dt, Node parentNode, string ParentId)
        {
            System.Data.DataView viewItem = new System.Data.DataView(dt);
            viewItem.RowFilter = "ParentId='" + ParentId+"'";
            foreach (System.Data.DataRowView childView in viewItem)
            {
                Node node = new Node { id = childView["Id"].ToString(), text = childView["text"].ToString() };
                parentNode.children.Add(node);
                string pId = childView["Id"].ToString();
                AddChildItems(dt, node, pId);
            }
        }

        class Node
        {
            public Node()
            {
                children = new System.Collections.Generic.List<Node>();
            }

            public string id { get; set; }
            public string text { get; set; }
            public System.Collections.Generic.List<Node> children { get; set; }
        }

    }




}