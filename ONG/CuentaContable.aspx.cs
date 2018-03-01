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
    public partial class _CuentaContable : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        
        [WebMethod(EnableSession = true)]
        public static object ObtieneListaCC(string search, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            amislEntities context = new amislEntities ();
            try
            {
                string prop = jtSorting.Replace("ASC", "").Replace("DESC", "").Trim();

                var lista = from g in context.ctacontable
                            join n in context.nivel on g.NIVEL equals n.UUID
                            orderby g.IDENTIFICADOR
                            //where g.CODIGO.Contains(search) || g.IDENTIFICADOR.Contains(search) || g.TIPO.Contains(search)
                            select new { g.UUID,g.CODIGO,g.IDENTIFICADOR,g.TIPO,g.NIVEL,n.NIVEL1};

                if (!string.IsNullOrEmpty(search))
                {
                    lista = lista.Where(g => g.CODIGO.Contains(search) || g.IDENTIFICADOR.Contains(search) || g.TIPO.Contains(search) || g.NIVEL1.Contains(search));
                }

                #region Orden
                if (jtSorting.Contains("ASC"))
                {
                    switch (prop)
                    {
                        case "CODIGO":
                            { lista = lista.OrderBy(x => x.CODIGO); break; }

                        case "IDENTIFICADOR":
                            { lista = lista.OrderBy(x => x.IDENTIFICADOR); break; }

                        case "TIPO":
                            { lista = lista.OrderBy(x => x.TIPO); break; }


                        case "NIVEL":
                            { lista = lista.OrderBy(x => x.NIVEL1); break; }

                    }
                }

                if (jtSorting.Contains("DESC"))
                {
                    switch (prop)
                    {
                        case "CODIGO":
                            { lista = lista.OrderByDescending(x => x.CODIGO); break; }

                        case "IDENTIFICADOR":
                            { lista = lista.OrderByDescending(x => x.IDENTIFICADOR); break; }

                        case "TIPO":
                            { lista = lista.OrderByDescending(x => x.TIPO); break; }

                        case "NIVEL":
                            { lista = lista.OrderByDescending(x => x.NIVEL1); break; }

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
        public static object Create(CuentaContable record)
        {
            amislEntities  context = new amislEntities ();
            try
            {
                ctacontable n = new ctacontable
                {
                    UUID = Guid.NewGuid().ToString().Replace("-", "").Trim().ToUpper(),
                    CODIGO = record.codigo,
                    IDENTIFICADOR = record.identificador,
                    TIPO = record.tipo,
                    NIVEL = record.nivel
                };
                context.ctacontable.Add(n);
                context.SaveChanges();
                return new { Result = "OK", Record = record };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object Edit(CuentaContable record)
        {
            amislEntities  context = new amislEntities ();
            try
            {
                ctacontable n = context.ctacontable.First(i => i.UUID == record.uuid);
                {
                    n.CODIGO = record.codigo;
                    n.IDENTIFICADOR = record.identificador;
                    n.TIPO = record.tipo;
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
                ctacontable n = context.ctacontable.First(i => i.UUID == UUID);
                context.ctacontable.Remove(n);
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
    }
}