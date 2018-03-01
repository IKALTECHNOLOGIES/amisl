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
    public partial class _Estadistica : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        
        [WebMethod(EnableSession = true)]
        public static object ObtieneListaEstadistica(string search, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            amislEntities context = new amislEntities ();
            try
            {
                string prop = jtSorting.Replace("ASC", "").Replace("DESC", "").Trim();

                var lista = from g in context.estadistica
                                join t in context.tipo on g.UUID_TIPO equals t.UUID
                                orderby g.IDENTIFICADOR
                                select new { g.UUID,g.IDENTIFICADOR,g.UUID_TIPO,T_IDENTIFICADOR = t.IDENTIFICADOR};

                if (!string.IsNullOrEmpty(search))
                {
                    lista = lista.Where(x => x.IDENTIFICADOR.Contains(search) || x.T_IDENTIFICADOR.Contains(search));
                }

                #region Orden
                if (jtSorting.Contains("ASC"))
                {
                    switch (prop)
                    {
                        case "IDENTIFICADOR":
                            { lista = lista.OrderBy(x => x.IDENTIFICADOR); break; }

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

                        case "UUID_TIPO":
                            { lista = lista.OrderByDescending(x => x.T_IDENTIFICADOR); break; }

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
        public static object Create(Estadistica record)
        {
            amislEntities  context = new amislEntities ();
            try
            {
                estadistica n = new estadistica
                {
                    UUID = Guid.NewGuid().ToString().Replace("-", "").Trim().ToUpper(),
                    IDENTIFICADOR = record.identificador,
                    UUID_TIPO = record.uuid_tipo
                };
                context.estadistica.Add(n);
                context.SaveChanges();
                return new { Result = "OK", Record = record };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object Edit(Estadistica record)
        {
            amislEntities  context = new amislEntities ();
            try
            {
                estadistica n = context.estadistica.First(i => i.UUID == record.uuid);
                {
                    n.IDENTIFICADOR = record.identificador;
                    n.UUID_TIPO = record.uuid_tipo;
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
                estadistica n = context.estadistica.First(i => i.UUID == UUID);
                context.estadistica.Remove(n);
                context.SaveChanges();
                return new { Result = "OK", Record = UUID };
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