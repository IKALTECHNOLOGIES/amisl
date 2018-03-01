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
    public partial class _Tipo : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        
        [WebMethod(EnableSession = true)]
        public static object ObtieneListaTipo(string search, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            amislEntities context = new amislEntities ();
            try
            { 
                string prop = jtSorting.Replace("ASC", "").Replace("DESC", "").Trim();
               
                var lista = from g in context.tipo
                            join n in context.nivel on g.NIVEL equals n.UUID
                            orderby g.TIPO1
                            //where g.IDENTIFICADOR.Contains(search) //|| SqlFunctions.StringConvert((Decimal)(g.TIPO1)).StartsWith(search)
                            select new { g.UUID, g.IDENTIFICADOR, g.ING_EGR_EST, 
                                CATEGORIA = g.ING_EGR_EST==1 ? "Ingreso" : 
                                g.ING_EGR_EST==2 ? "Gasto" :
                                g.ING_EGR_EST==3 ? "Estadística": "",g.TIPO1, g.NIVEL,n.NIVEL1 };

                if (!string.IsNullOrEmpty(search))
                {
                    lista = lista.Where(x => x.IDENTIFICADOR.Contains(search) || x.NIVEL1.Contains(search) || x.CATEGORIA.Contains(search));
                }

                #region Orden 
                if (jtSorting.Contains("ASC")){
                    switch (prop)
                    {
                        case "IDENTIFICADOR":
                            { lista = lista.OrderBy(x => x.IDENTIFICADOR); break; }

                        case "TIPO1":
                            { lista = lista.OrderBy(x => x.TIPO1); break; }

                        case "NIVEL":
                            { lista = lista.OrderBy(x => x.NIVEL1); break; }

                        case "ING_EGR_EST":
                            { lista = lista.OrderBy(x => x.ING_EGR_EST); break; }

                    }    
                }

                if (jtSorting.Contains("DESC"))
                {
                    switch (prop)
                    {
                        case "IDENTIFICADOR":
                            { lista = lista.OrderByDescending(x => x.IDENTIFICADOR); break; }

                        case "TIPO1":
                            { lista = lista.OrderByDescending(x => x.TIPO1); break; }

                        case "NIVEL":
                            { lista = lista.OrderByDescending(x => x.NIVEL1); break; }

                        case "ING_EGR_EST":
                            { lista = lista.OrderByDescending(x => x.ING_EGR_EST); break; }
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
        public static object Create(Tipo record)
        {
            amislEntities  context = new amislEntities ();
            try
            {
                tipo n = new tipo
                {
                    UUID = Guid.NewGuid().ToString().Replace("-", "").Trim().ToUpper(),
                    IDENTIFICADOR = record.identificador,
                    TIPO1 = record.tipo1,
                    NIVEL = record.nivel,
                    ING_EGR_EST = record.ing_egr_est
                };
                context.tipo.Add(n);
                context.SaveChanges();
                return new { Result = "OK", Record = record };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object Edit(Tipo record)
        {
            amislEntities  context = new amislEntities ();
            try
            {
                tipo n = context.tipo.First(i => i.UUID == record.uuid);
                {
                    n.IDENTIFICADOR = record.identificador;
                    n.TIPO1 = record.tipo1;
                    n.NIVEL = record.nivel;
                    n.ING_EGR_EST = record.ing_egr_est;
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
                tipo n = context.tipo.First(i => i.UUID == UUID);
                context.tipo.Remove(n);
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