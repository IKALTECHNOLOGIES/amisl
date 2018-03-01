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
using System.Globalization;
using System.Data.Entity.Validation;
using System.Diagnostics;


namespace ONG
{
    public partial class _IngresoRepFinanciero : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        
        [WebMethod(EnableSession = true)]
        public static object ObtieneListaIngresoRep(string search, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            amislEntities context = new amislEntities ();
            try
            {
                string prop = jtSorting.Replace("ASC", "").Replace("DESC", "").Trim();

                var lista = from g in context.rep_financiero
                            join n in context.nivel on g.NIVEL equals n.UUID
                            orderby g.FECHA
                            select new { g.UUID, g.MES, g.ANIO, g.FECHA, g.SALDOANTERIOR, g.TASACAMBIO, g.NIVEL, n.NIVEL1, SALDODOLARES = decimal.Round((g.SALDOANTERIOR / g.TASACAMBIO).Value,2) };
        
                if (!string.IsNullOrEmpty(search))
                {
                    lista = lista.Where(x => Convert.ToString(x.MES).Contains(search) || x.NIVEL1.Contains(search) || Convert.ToString(x.ANIO).Contains(search) || Convert.ToString(x.FECHA).Contains(search));
                }

                #region Orden
                if (jtSorting.Contains("ASC"))
                {
                    switch (prop)
                    {
                        case "MES":
                            { lista = lista.OrderBy(x => x.MES); break; }

                        case "NIVEL":
                            { lista = lista.OrderBy(x => x.NIVEL1); break; }

                        case "ANIO":
                            { lista = lista.OrderBy(x => x.ANIO); break; }

                        case "FECHA":
                            { lista = lista.OrderBy(x => x.FECHA); break; }

                    }
                }

                if (jtSorting.Contains("DESC"))
                {
                    switch (prop)
                    {
                        case "MES":
                            { lista = lista.OrderByDescending(x => x.MES); break; }

                        case "NIVEL":
                            { lista = lista.OrderByDescending(x => x.NIVEL1); break; }

                        case "ANIO":
                            { lista = lista.OrderByDescending(x => x.ANIO); break; }

                        case "FECHA":
                            { lista = lista.OrderByDescending(x => x.FECHA); break; }

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
        public static object Create(IngresoRepFinanciero record)
        {
            amislEntities  context = new amislEntities ();

            //var dt = DateTime.ParseExact(record.fecha.ToShortDateString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            //var formatted = dt.ToString("yyyy-MM-dd");

            try
            {
                rep_financiero n = new rep_financiero
                {
                    UUID = Guid.NewGuid().ToString().Replace("-", "").Trim().ToUpper(),
                    MES = record.mes,
                    ANIO = record.anio,
                    FECHA = record.fecha,
                    SALDOANTERIOR = record.saldoanterior,
                    TASACAMBIO = record.tasacambio,
                    NIVEL = record.nivel
                };
                context.rep_financiero.Add(n);
                context.SaveChanges();
                return new { Result = "OK", Record = record };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object Edit(IngresoRepFinanciero record)
        {
            amislEntities  context = new amislEntities ();
            try
            {
                rep_financiero n = context.rep_financiero.First(i => i.UUID == record.uuid);
                {
                    n.MES = record.mes;
                    n.ANIO = record.anio;
                    n.FECHA = record.fecha;
                    n.SALDOANTERIOR = record.saldoanterior;
                    n.TASACAMBIO = record.tasacambio;
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
                rep_financiero n = context.rep_financiero.First(i => i.UUID == UUID);
                context.rep_financiero.Remove(n);
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

                var lista = from g in context.ctacontable where g.TIPO.Equals("I") 
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

        #region Tabla INGRESO_REP

        [WebMethod(EnableSession = true)]
        public static object ListIngreso(string UUID_REP_FINANCIERO)
        {
            amislEntities context = new amislEntities();
            try
            {
                
                var lista = from g in context.ingreso_rep
                            join i in context.rep_financiero on g.UUID_REP_FINANCIERO equals i.UUID
                            orderby g.UUID
                            where g.UUID_REP_FINANCIERO.Equals(UUID_REP_FINANCIERO)
                            select new { g.UUID, g.MONTO, g.UUID_REP_FINANCIERO, g.UUID_INGRESO, MONTODOLARES = decimal.Round((g.MONTO / i.TASACAMBIO).Value, 2) };

                int total = lista.Count();


                return new { Result = "OK", Records = lista, TotalRecordCount = total };

            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object ObtieneOpcionesIngreso()
        {
            amislEntities context = new amislEntities();
            try
            {

                var lista = from g in context.ingreso select g;
                //int total = lista.Count();
                var tipos = lista.Select(c => new { DisplayText = c.IDENTIFICADOR, Value = c.UUID }).OrderBy(x => x.DisplayText);
                return new { Result = "OK", Options = tipos };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object CreateIngreso(IngresoGastoRep record)
        {
            amislEntities context = new amislEntities();

            try
            {
                ingreso_rep n = new ingreso_rep
                {
                    UUID = Guid.NewGuid().ToString().Replace("-", "").Trim().ToUpper(),
                    MONTO = record.monto,
                    UUID_INGRESO = record.uuid_ingreso,
                    UUID_REP_FINANCIERO = record.uuid_rep_financiero
                };
                context.ingreso_rep.Add(n);
                context.SaveChanges();

                return new { Result = "OK", Record = record };
            }
            //catch (Exception ex)
            //{
            //    return new { Result = "ERROR", Message = ex.Message };
            //}
            catch (DbEntityValidationException dbEx)
            {
                string trace="";
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        trace = string.Format("Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
                return new { Result = "ERROR", Message = trace };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object EditIngreso(IngresoGastoRep record)
        {
            amislEntities context = new amislEntities();
            try
            {
                ingreso_rep n = context.ingreso_rep.First(i => i.UUID == record.uuid);
                {
                    n.MONTO = record.monto;
                    n.UUID_INGRESO = record.uuid_ingreso;
                    n.UUID_REP_FINANCIERO = record.uuid_rep_financiero;
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
        public static object DeleteIngreso(String UUID)
        {
            amislEntities context = new amislEntities();
            try
            {
                ingreso_rep n = context.ingreso_rep.First(i => i.UUID == UUID);
                context.ingreso_rep.Remove(n);
                context.SaveChanges();
                return new { Result = "OK", Record = UUID };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod]
        public static string TotalesReporte(string UUID_REP_FINANCIERO, string TASACAMBIO)
        {
            string sumIngreso = "";
            string sumIngresoDolares = "";
            string sumGasto = "";
            string sumGastoDolares = "";

            amislEntities context = new amislEntities();
            amislEntities context2 = new amislEntities();
            
            try
            {
                var lista = (from i in context.ingreso_rep
                            where i.UUID_REP_FINANCIERO.Equals(UUID_REP_FINANCIERO)
                            select i.MONTO).ToList(); 

                sumIngreso = lista.Sum().ToString("0.00");
                sumIngresoDolares = (lista.Sum() / Convert.ToDecimal(TASACAMBIO)).ToString("0.00");
                                      
            }
            catch (Exception ex)
            {
                
            }

            try
            {
                var lista = (from i in context2.gasto_rep
                            where i.UUID_REP_FINANCIERO.Equals(UUID_REP_FINANCIERO)
                            select i.MONTO).ToList(); 

                sumGasto = lista.Sum().ToString("0.00");
                sumGastoDolares = (lista.Sum() / Convert.ToDecimal(TASACAMBIO)).ToString("0.00");
                                      
            }
            catch (Exception ex)
            {
                
            }

            string texto = "Total Ingresos:     " + sumIngreso  + "  (US$ "+ sumIngresoDolares +") "+    
                            "Total Gastos:      " + sumGasto + "  (US$ " + sumGastoDolares + ") ";
                            
            return texto;
        }

        #endregion  

        #region Tabla GASTO_REP

        [WebMethod(EnableSession = true)]
        public static object ListGasto(string UUID_REP_FINANCIERO)
        {
            amislEntities context = new amislEntities();
            try
            {

                var lista = from g in context.gasto_rep
                            join i in context.rep_financiero on g.UUID_REP_FINANCIERO equals i.UUID
                            orderby g.UUID
                            where g.UUID_REP_FINANCIERO.Equals(UUID_REP_FINANCIERO)
                            select new { g.UUID, g.MONTO, g.UUID_REP_FINANCIERO, g.UUID_GASTO, MONTODOLARES = decimal.Round((g.MONTO / i.TASACAMBIO).Value, 2) };

                int total = lista.Count();


                return new { Result = "OK", Records = lista, TotalRecordCount = total };

            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object ObtieneOpcionesGasto()
        {
            amislEntities context = new amislEntities();
            try
            {

                var lista = from g in context.gasto select g;
                //int total = lista.Count();
                var tipos = lista.Select(c => new { DisplayText = c.IDENTIFICADOR, Value = c.UUID }).OrderBy(x => x.DisplayText);
                return new { Result = "OK", Options = tipos };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object CreateGasto(IngresoGastoRep record)
        {
            amislEntities context = new amislEntities();

            try
            {
                gasto_rep n = new gasto_rep
                {
                    UUID = Guid.NewGuid().ToString().Replace("-", "").Trim().ToUpper(),
                    MONTO = record.monto,
                    UUID_GASTO = record.uuid_gasto,
                    UUID_REP_FINANCIERO = record.uuid_rep_financiero
                };
                context.gasto_rep.Add(n);
                context.SaveChanges();
                return new { Result = "OK", Record = record };
            }
            //catch (Exception ex)
            //{
            //    return new { Result = "ERROR", Message = ex.Message };
            //}
            catch (DbEntityValidationException dbEx)
            {
                string trace = "";
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        trace = string.Format("Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
                return new { Result = "ERROR", Message = trace };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object EditGasto(IngresoGastoRep record)
        {
            amislEntities context = new amislEntities();
            try
            {
                gasto_rep n = context.gasto_rep.First(i => i.UUID == record.uuid);
                {
                    n.MONTO = record.monto;
                    n.UUID_GASTO = record.uuid_gasto;
                    n.UUID_REP_FINANCIERO = record.uuid_rep_financiero;
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
        public static object DeleteGasto(String UUID)
        {
            amislEntities context = new amislEntities();
            try
            {
                gasto_rep n = context.gasto_rep.First(i => i.UUID == UUID);
                context.gasto_rep.Remove(n);
                context.SaveChanges();
                return new { Result = "OK", Record = UUID };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        #endregion  

        #region Tabla REP_ESTADISTICA

        [WebMethod(EnableSession = true)]
        public static object ListEstadistica(string UUID_REP_FINANCIERO)
        {
            amislEntities context = new amislEntities();
            try
            {

                var lista = from g in context.rep_estadistica
                            //join i in context.ingreso on g.UUID_INGRESO equals i.UUID
                            orderby g.UUID
                            where g.UUID_REP_FINANCIERO.Equals(UUID_REP_FINANCIERO)
                            select new { g.UUID, g.VALOR, g.UUID_REP_FINANCIERO, g.UUID_ESTADISTICA };

                int total = lista.Count();


                return new { Result = "OK", Records = lista, TotalRecordCount = total };

            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object ObtieneOpcionesEstadistica()
        {
            amislEntities context = new amislEntities();
            try
            {

                var lista = from g in context.estadistica select g;
                //int total = lista.Count();
                var tipos = lista.Select(c => new { DisplayText = c.IDENTIFICADOR, Value = c.UUID }).OrderBy(x => x.DisplayText);
                return new { Result = "OK", Options = tipos };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object CreateEstadistica(IngresoGastoRep record)
        {
            amislEntities context = new amislEntities();

            try
            {
                rep_estadistica n = new rep_estadistica
                {
                    UUID = Guid.NewGuid().ToString().Replace("-", "").Trim().ToUpper(),
                    VALOR = record.valor,
                    UUID_ESTADISTICA = record.uuid_estadistica,
                    UUID_REP_FINANCIERO = record.uuid_rep_financiero
                };
                context.rep_estadistica.Add(n);
                context.SaveChanges();
                return new { Result = "OK", Record = record };
            }
            //catch (Exception ex)
            //{
            //    return new { Result = "ERROR", Message = ex.Message };
            //}
            catch (DbEntityValidationException dbEx)
            {
                string trace = "";
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        trace = string.Format("Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
                return new { Result = "ERROR", Message = trace };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object EditEstadistica(IngresoGastoRep record)
        {
            amislEntities context = new amislEntities();
            try
            {
                rep_estadistica n = context.rep_estadistica.First(i => i.UUID == record.uuid);
                {
                    n.VALOR = record.valor;
                    n.UUID_ESTADISTICA = record.uuid_estadistica;
                    n.UUID_REP_FINANCIERO = record.uuid_rep_financiero;
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
        public static object DeleteEstadistica(String UUID)
        {
            amislEntities context = new amislEntities();
            try
            {
                rep_estadistica n = context.rep_estadistica.First(i => i.UUID == UUID);
                context.rep_estadistica.Remove(n);
                context.SaveChanges();
                return new { Result = "OK", Record = UUID };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        #endregion  
    }
}