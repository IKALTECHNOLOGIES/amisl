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
    public partial class _Nivel : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        
        [WebMethod(EnableSession = true)]
        public static object ObtieneListaNivel(int jtStartIndex = 0, int jtPageSize = 0,string jtSorting = null)
        {
            amislEntities context = new amislEntities ();
            try
            {

                var lista = from g in context.nivel select new { g.UUID,g.NIVEL1,g.IDENTIFICADOR,g.SUPERIOR};
                int total = lista.Count();
                return new { Result = "OK", Records = lista.OrderBy(x => x.NIVEL1).ToList(), TotalRecordCount = total };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object Create(Nivel record)
        {
            amislEntities  context = new amislEntities ();
            try
            {
                nivel n = new nivel
                {
                    UUID = Guid.NewGuid().ToString().Replace("-", "").Trim().ToUpper(),
                    NIVEL1 = record.nivel1,
                    IDENTIFICADOR = record.identificador,
                    SUPERIOR = record.superior
                };
                context.nivel.Add(n);
                context.SaveChanges();
                return new { Result = "OK", Record = record };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object Edit(Nivel record)
        {
            amislEntities  context = new amislEntities ();
            try
            {
                nivel n = context.nivel.First(i => i.UUID == record.uuid);
                {
                    n.NIVEL1 = record.nivel1;
                    n.IDENTIFICADOR = record.identificador;
                    n.SUPERIOR = record.superior;
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
    }
}