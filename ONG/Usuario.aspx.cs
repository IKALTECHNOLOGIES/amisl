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
using ONG.Utilitarios;


namespace ONG
{
    public partial class _Usuario : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        
        [WebMethod(EnableSession = true)]
        public static object ObtieneListaUsuario(string search, int jtStartIndex = 0, int jtPageSize = 0,string jtSorting = null)
        {
            amislEntities context = new amislEntities ();
            try
            {
                string prop = jtSorting.Replace("ASC", "").Replace("DESC", "").Trim();
                
                var lista = from g in context.usuario 
                            join n in context.nivel on g.NIVEL equals n.UUID 
                            select new {g.NOMBRE,g.APELLIDOS,g.USUARIO1,g.TEL1,g.EMAIL,g.DIR1,g.NIVEL,n.NIVEL1};

                if (!string.IsNullOrEmpty(search))
                {
                    lista = lista.Where(g => g.NOMBRE.Contains(search) || g.APELLIDOS.Contains(search) || g.USUARIO1.Contains(search)
                        || g.TEL1.Contains(search) || g.EMAIL.Contains(search) || g.DIR1.Contains(search) || g.NIVEL1.Contains(search));
                }

                #region Orden
                if (jtSorting.Contains("ASC"))
                {
                    switch (prop)
                    {
                        case "NOMBRE":
                            { lista = lista.OrderBy(x => x.NOMBRE ); break; }

                        case "APELLIDOS":
                            { lista = lista.OrderBy(x => x.APELLIDOS); break; }

                        case "USUARIO1":
                            { lista = lista.OrderBy(x => x.USUARIO1); break; }

                        case "TEL1":
                            { lista = lista.OrderBy(x => x.TEL1); break; }

                        case "EMAIL":
                            { lista = lista.OrderBy(x => x.EMAIL); break; }

                        case "DIR1":
                            { lista = lista.OrderBy(x => x.DIR1); break; }

                        case "NIVEL":
                            { lista = lista.OrderBy(x => x.NIVEL1); break; }

                    }
                }

                if (jtSorting.Contains("DESC"))
                {
                    switch (prop)
                    {
                        case "NOMBRE":
                            { lista = lista.OrderByDescending(x => x.NOMBRE); break; }

                        case "APELLIDOS":
                            { lista = lista.OrderByDescending(x => x.APELLIDOS); break; }

                        case "USUARIO1":
                            { lista = lista.OrderByDescending(x => x.USUARIO1); break; }

                        case "TEL1":
                            { lista = lista.OrderByDescending(x => x.TEL1); break; }

                        case "EMAIL":
                            { lista = lista.OrderByDescending(x => x.EMAIL); break; }

                        case "DIR1":
                            { lista = lista.OrderByDescending(x => x.DIR1); break; }

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
        public static object Create(Usuario record)
        {
            amislEntities  context = new amislEntities ();
            string uuidUsuario = Guid.NewGuid().ToString().Replace("-", "").Trim().ToUpper();
            try
            {
                //tabla usuario
                usuario n = new usuario
                {
                    UUID = uuidUsuario,
                    NOMBRE = record.nombre,
                    APELLIDOS = record.apellidos,
                    USUARIO1 = record.usuario1,
                    TEL1 = record.tel1,
                    TEL2 = record.tel2,
                    TEL3 = record.tel3,
                    EMAIL = record.email,
                    DIR1 = record.dir1,
                    DIR2 = record.dir2,
                    DIR3 = record.dir3,
                    NIVEL = record.nivel,
                    CLAVE = SecurePasswordHasher.Hash(record.clave)
                };

                context.usuario.Add(n);
                context.SaveChanges();

                amislEntities contextN = new amislEntities();

                //tabla intermedia nivel_usuario
                nivel_usuario nu = new nivel_usuario
                {
                    UUID = Guid.NewGuid().ToString().Replace("-", "").Trim().ToUpper(),
                    UUID_USUARIO = uuidUsuario,
                    UUID_NIVEL = record.nivel

                };

                contextN.nivel_usuario.Add(nu);
                contextN.SaveChanges();

                return new { Result = "OK", Record = record };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object Edit(Usuario record)
        {
            amislEntities  context = new amislEntities ();
            try
            {
                usuario n = context.usuario.First(i => i.UUID == record.uuid);
                {
                    n.NOMBRE = record.nombre;
                    n.APELLIDOS = record.apellidos;
                    n.USUARIO1 = record.usuario1;
                    n.TEL1 = record.tel1;
                    n.TEL2 = record.tel2;
                    n.TEL3 = record.tel3;
                    n.EMAIL = record.email;
                    n.DIR1 = record.dir1;
                    n.DIR2 = record.dir2;
                    n.DIR3 = record.dir3;
                    n.NIVEL = record.nivel;
                    n.CLAVE = SecurePasswordHasher.Hash(record.clave); 
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
                usuario n = context.usuario.First(i => i.UUID == UUID);
                context.usuario.Remove(n);
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
                var niveles = lista.Select(c => new { DisplayText = c.IDENTIFICADOR , Value = c.NIVEL1 }).OrderBy(x => x.DisplayText);
                return new { Result = "OK", Options = niveles };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }
    }
}