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
    public partial class _Etiqueta : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        
        [WebMethod(EnableSession = true)]
        public static object ObtieneListaEtiqueta(string search,int jtStartIndex = 0, int jtPageSize = 0,string jtSorting = null)
        {
            amislEntities context = new amislEntities ();
            try
            {
                string prop = jtSorting.Replace("ASC", "").Replace("DESC", "").Trim();

                var lista = from g in context.etiqueta
                                join i in context.idioma
                                    on g.UUID_IDIOMA equals i.UUID
                                //where g.VALOR.Contains(search) || g.IDENTIFICADOR.Contains(search) || i.NOMBRE.Contains(search)
                                select new { g.UUID, g.VALOR, g.UUID_IDIOMA,i.NOMBRE, g.UUID_REGISTRO, g.IDENTIFICADOR };

                if (!string.IsNullOrEmpty(search))
                {
                    lista = lista.Where(x => x.VALOR.Contains(search) || x.IDENTIFICADOR.Contains(search) || x.NOMBRE.Contains(search));
                }

                #region Orden
                if (jtSorting.Contains("ASC"))
                {
                    switch (prop)
                    {
                        case "VALOR":
                            { lista = lista.OrderBy(x => x.VALOR); break; }

                        case "UUID_IDIOMA":
                            { lista = lista.OrderBy(x => x.NOMBRE); break; }

                        case "UUID_REGISTRO":
                            { lista = lista.OrderBy(x => x.UUID_REGISTRO); break; }

                        case "IDENTIFICADOR":
                            { lista = lista.OrderBy(x => x.IDENTIFICADOR); break; }

                    }
                }

                if (jtSorting.Contains("DESC"))
                {
                    switch (prop)
                    {
                        case "VALOR":
                            { lista = lista.OrderByDescending(x => x.VALOR); break; }

                        case "UUID_IDIOMA":
                            { lista = lista.OrderByDescending(x => x.NOMBRE); break; }

                        case "UUID_REGISTRO":
                            { lista = lista.OrderByDescending(x => x.UUID_REGISTRO); break; }

                        case "IDENTIFICADOR":
                            { lista = lista.OrderBy(x => x.IDENTIFICADOR); break; }

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
        public static object Create(Etiqueta record)
        {
            amislEntities  context = new amislEntities ();
            try
            {
                etiqueta n = new etiqueta
                {
                    UUID = Guid.NewGuid().ToString().Replace("-", "").Trim().ToUpper(),
                    VALOR = record.valor,
                    UUID_IDIOMA = record.uuid_idioma,
                    UUID_REGISTRO = record.uuid_registro,
                    IDENTIFICADOR = record.identificador
                };
                context.etiqueta.Add(n);
                context.SaveChanges();
                return new { Result = "OK", Record = record };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object Edit(Etiqueta record)
        {
            amislEntities  context = new amislEntities ();
            try
            {
                etiqueta n = context.etiqueta.First(i => i.UUID == record.uuid);
                {
                    n.VALOR = record.valor;
                    n.UUID_IDIOMA = record.uuid_idioma;
                    n.UUID_REGISTRO = record.uuid_registro;
                    n.IDENTIFICADOR = record.identificador;
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
                etiqueta n = context.etiqueta.First(i => i.UUID == UUID);
                context.etiqueta.Remove(n);
                context.SaveChanges();
                return new { Result = "OK", Record = UUID };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object ObtieneOpcionesIdioma()
        {
            amislEntities context = new amislEntities();
            try
            {

                var lista = from g in context.idioma select g;
                //int total = lista.Count();
                var idiomas = lista.Select(c => new { DisplayText = c.NOMBRE, Value = c.UUID }).OrderBy(x => x.DisplayText);
                return new { Result = "OK", Options = idiomas };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }
    }
}