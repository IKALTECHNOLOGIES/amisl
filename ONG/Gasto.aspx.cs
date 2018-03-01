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
using System.Data.Entity;
using System.Data.SqlClient;


namespace ONG
{
    public partial class _Gasto : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
         
        
        [WebMethod(EnableSession = true)]
        public static object ObtieneListaGasto(string search, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            amislEntities context = new amislEntities ();
            try
            {
                string prop = jtSorting.Replace("ASC", "").Replace("DESC", "").Trim();

                var lista = from g in context.gasto
                            join n in context.nivel on g.NIVEL equals n.UUID
                            join t in context.tipo on g.UUID_TIPO equals t.UUID
                            join cc in context.ctacontable on g.UUID_CTACONTABLE equals cc.UUID
                            orderby g.IDENTIFICADOR
                            select new { g.UUID, g.IDENTIFICADOR, g.UUID_CTACONTABLE,g.UUID_TIPO,g.NIVEL,n.NIVEL1, CC_IDENTIFICADOR = cc.IDENTIFICADOR,T_IDENTIFICADOR = t.IDENTIFICADOR };

                if (!string.IsNullOrEmpty(search))
                {
                    lista = lista.Where(x => x.IDENTIFICADOR.Contains(search) || x.NIVEL1.Contains(search) || x.CC_IDENTIFICADOR.Contains(search) || x.T_IDENTIFICADOR.Contains(search));
                }

                
                #region Orden 
                if (jtSorting.Contains("ASC")){
                    switch (prop)
                    {
                        case "IDENTIFICADOR":
                            { lista = lista.OrderBy(x => x.IDENTIFICADOR); break; }

                        case "NIVEL":
                            { lista = lista.OrderBy(x => x.NIVEL1); break; }

                        case "UUID_CTACONTABLE":
                            { lista = lista.OrderBy(x => x.CC_IDENTIFICADOR); break; }

                        case "UUID_TIPO":
                            { lista = lista.OrderBy(x => x.T_IDENTIFICADOR); break; }

                    }    
                }

                if (jtSorting.Contains("DESC"))
                {
                    switch (prop)
                    {
                        case "IDENTIFICADOR":
                            { lista = lista.OrderByDescending(x => x.IDENTIFICADOR); break; }

                        case "NIVEL":
                            { lista = lista.OrderByDescending(x => x.NIVEL1); break; }

                        case "UUID_CTACONTABLE":
                            { lista = lista.OrderByDescending(x => x.CC_IDENTIFICADOR); break; }

                        case "UUID_TIPO":
                            { lista = lista.OrderByDescending(x => x.T_IDENTIFICADOR); break; }

                    }
                }
                #endregion 
                

                int total = lista.Count();

                lista = lista.Skip(jtStartIndex).Take(jtPageSize);

                return new { Result = "OK", Records = lista , TotalRecordCount = total };    
                
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object Create(Gasto record)
        {
            amislEntities  context = new amislEntities ();
            try
            {
                gasto n = new gasto
                {
                    UUID = Guid.NewGuid().ToString().Replace("-", "").Trim().ToUpper(),
                    IDENTIFICADOR = record.identificador,
                    UUID_CTACONTABLE = record.uuid_ctacontable,
                    UUID_TIPO = record.uuid_tipo,
                    NIVEL = record.nivel
                };
                context.gasto.Add(n);
                context.SaveChanges();
                return new { Result = "OK", Record = record };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object Edit(Gasto record)
        {
            amislEntities  context = new amislEntities ();
            try
            {
                gasto n = context.gasto.First(i => i.UUID == record.uuid);
                {
                    n.IDENTIFICADOR = record.identificador;
                    n.UUID_CTACONTABLE = record.uuid_ctacontable;
                    n.UUID_TIPO = record.uuid_tipo;
                    n.NIVEL = record.nivel;
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
                gasto n = context.gasto.First(i => i.UUID == UUID);
                context.gasto.Remove(n);
                context.SaveChanges();
                return new { Result = "OK", Record = UUID };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object ObtieneOpcionesNivel()
        {
            amislEntities context = new amislEntities();
            try
            {

                var lista = from g in context.nivel select g;
                //int total = lista.Count();
                var niveles = lista.Select(c => new { DisplayText = c.IDENTIFICADOR, Value = c.UUID }).OrderBy(x => x.DisplayText);
                return new { Result = "OK", Options = niveles };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object ObtieneOpcionesCC()
        {
            amislEntities context = new amislEntities();
            try
            {

                var lista = from g in context.ctacontable where g.TIPO.Equals("G") 
                            select g;
                //int total = lista.Count();
                var cuentas = lista.Select(c => new { DisplayText = c.IDENTIFICADOR, Value = c.UUID }).OrderBy(x => x.DisplayText);
                return new { Result = "OK", Options = cuentas };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object ObtieneOpcionesTipo()
        {
            amislEntities context = new amislEntities();
            try
            {

                var lista = from g in context.tipo select g;
                //int total = lista.Count();
                var tipos = lista.Select(c => new { DisplayText = c.IDENTIFICADOR, Value = c.UUID }).OrderBy(x => x.DisplayText);
                return new { Result = "OK", Options = tipos };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }
    }
}