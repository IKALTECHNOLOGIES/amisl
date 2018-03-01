using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.Entity;
using System.ComponentModel;
using ONG.Models;
using System.Web.Mvc;
using ONG.Entities;
using System.Web.Services;



namespace ONG
{
    public partial class _Idioma : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        
        [WebMethod(EnableSession = true)]
        public static object ObtieneListaIdioma(string search, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            amislEntities context = new amislEntities ();
            try
            {
                string prop = jtSorting.Replace("ASC", "").Replace("DESC", "").Trim();

                var lista = from g in context.idioma
                            //where g.NOMBRE.Contains(search) || g.ABREVIATURA.Contains(search)
                            select new { g.UUID, g.NOMBRE, g.ABREVIATURA };
                
                if (!string.IsNullOrEmpty(search))
                {
                    lista = lista.Where(x => x.ABREVIATURA.Contains(search) || x.NOMBRE.Contains(search));
                }

                #region Orden
                if (jtSorting.Contains("ASC"))
                {
                    switch (prop)
                    {
                        case "NOMBRE":
                            { lista = lista.OrderBy(x => x.NOMBRE); break; }

                        case "ABREVIATURA":
                            { lista = lista.OrderBy(x => x.ABREVIATURA); break; }

                    }
                }

                if (jtSorting.Contains("DESC"))
                {
                    switch (prop)
                    {
                        case "NOMBRE":
                            { lista = lista.OrderByDescending(x => x.NOMBRE); break; }

                        case "ABREVIATURA":
                            { lista = lista.OrderByDescending(x => x.ABREVIATURA); break; }

                    }
                }
                #endregion 

                int total = lista.Count();

                lista = lista.Skip(jtStartIndex).Take(jtPageSize);

                return new { Result = "OK", Records = lista, TotalRecordCount = total };
                
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object Create(Idioma record)
        {
            amislEntities  context = new amislEntities ();
            try
            {
                idioma n = new idioma
                {
                    UUID = Guid.NewGuid().ToString().Replace("-", "").Trim().ToUpper(),
                    NOMBRE = record.nombre,
                    ABREVIATURA = record.abreviatura
                };
                context.idioma.Add(n);
                context.SaveChanges();
                return new { Result = "OK", Record = record };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object Edit(Idioma record)
        {
            amislEntities  context = new amislEntities ();
            try
            {
                idioma n = context.idioma.First(i => i.UUID == record.uuid);
                {
                    n.NOMBRE = record.nombre;
                    n.ABREVIATURA = record.abreviatura;
                    context.SaveChanges();
                };
                return new { Result = "OK", Record = record };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object Delete(String UUID)
        {
            amislEntities  context = new amislEntities ();
            try
            {
                idioma n = context.idioma.First(i => i.UUID == UUID);
                context.idioma.Remove(n);
                context.SaveChanges();
                return new { Result = "OK", Record = UUID };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }
    }
}