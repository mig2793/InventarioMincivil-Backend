using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Inventario;
using System.Web.Script.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace Webinventario
{

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Inventario : System.Web.Services.WebService
    {

        [WebMethod]
        public List<Stocktaking> GetMarcas()
        {
            List<Stocktaking> listTemple = new List<Stocktaking>();
            DataTable selectTable = new DataTable();

            Stocktaking Marcas = new Stocktaking();
            selectTable = Marcas.SelectMarca();

            foreach (DataRow row in selectTable.Rows)
            {
                listTemple.Add(new Stocktaking() { _id_Marcas = Int32.Parse(row["id"].ToString()), _Marcas = row["marca"].ToString()});
            }

            return listTemple;
        }
        [WebMethod]
        public List<Stocktaking> GetGrupos()
        {
            List<Stocktaking> listTemple = new List<Stocktaking>();
            DataTable selectTable = new DataTable();

            Stocktaking grupos = new Stocktaking();
            selectTable = grupos.SelectGrupo();

            foreach (DataRow row in selectTable.Rows)
            {
                listTemple.Add(new Stocktaking() { _clasificacion = Int32.Parse(row["Clsif"].ToString()), _nombreGrupo = row["NombreGr"].ToString() });
            }

            return listTemple;
        }
        [WebMethod]
        public List<Stocktaking> SelectLikeRIconstruye(string search, int condition)
        {
            List<Stocktaking> listTemple = new List<Stocktaking>();
            DataTable selectTable = new DataTable();

            Stocktaking RefrenceExist = new Stocktaking();
            selectTable = RefrenceExist.SelectLikeRIconstruye(search,condition);

            if (condition == 1)
            {
                foreach (DataRow row in selectTable.Rows)
                {
                    listTemple.Add(new Stocktaking() { _CodigoBarrasIcontruye = row["CodigoItem"].ToString(), _DescripcionItem = row["descripcionItem"].ToString(), _nombreGrupo = row["NombreGr"].ToString(), _clasificacion = Int32.Parse(row["Clasif"].ToString())});
                }
            }else if(condition == 2)
            {
                foreach (DataRow row in selectTable.Rows)
                {
                    listTemple.Add(new Stocktaking() { _CodigoBarrasIcontruye = row["CodigoItem"].ToString(), _DescripcionItem = row["descripcionItem"].ToString(), _nombreGrupo = row["NombreGr"].ToString() });
                }
            }

            if (listTemple.Count == 0)
            {
                selectTable = RefrenceExist.SelectLikeRIconstruyeAll(search, condition);

                    if (condition == 1)
                    {
                        foreach (DataRow row in selectTable.Rows)
                        {
                            listTemple.Add(new Stocktaking() { _CodigoBarrasIcontruye = row["CodigoItem"].ToString(), _DescripcionItem = row["descripcion"].ToString(), _nombreGrupo = row["NombreGr"].ToString(), _clasificacion = Int32.Parse(row["Clasif"].ToString()) });
                        }
                    }
                    else if (condition == 2)
                    {
                        foreach (DataRow row in selectTable.Rows)
                        {
                            listTemple.Add(new Stocktaking() { _CodigoBarrasIcontruye = row["CodigoItem"].ToString(), _DescripcionItem = row["descripcion"].ToString(), _nombreGrupo = row["NombreGr"].ToString() });
                        }
                    }
            }

            return listTemple;
        }
        [WebMethod]
        public List<Stocktaking> SelectLikeLocationItem(string codBarrasLocation)
        {
            List<Stocktaking> listTemple = new List<Stocktaking>();
            DataTable selectTable = new DataTable();

            Stocktaking LocationExist = new Stocktaking();
            selectTable = LocationExist.SelectLikeLocationItem(codBarrasLocation);

            foreach (DataRow row in selectTable.Rows)
            {
                listTemple.Add(new Stocktaking() { _Ubicacion = row["codigoUbicacion"].ToString()});
            }

            return listTemple;
        }
        [WebMethod]
        public List<Stocktaking> selectProyectos()
        {
            List<Stocktaking> listTemple = new List<Stocktaking>();
            DataTable selectTable = new DataTable();

            Stocktaking Projects = new Stocktaking();
            selectTable = Projects.selectProyectos();
            
            foreach (DataRow row in selectTable.Rows)
            {
                listTemple.Add(new Stocktaking() { _codProyectos = row["CodCCiConstruye"].ToString(), _Proyectos = row["DescripcióniContruye"].ToString()  });
            }

            return listTemple;
        }
        [WebMethod]
        public List<Stocktaking> selectEquivalences(string codBarras)
        {
            List<Stocktaking> listTemple = new List<Stocktaking>();
            DataTable selectTable = new DataTable();

            Stocktaking equivalences = new Stocktaking();
            selectTable = equivalences.selectEquivalences(codBarras);

            foreach (DataRow row in selectTable.Rows)
            {
                listTemple.Add(new Stocktaking() { _Equivalencia = row["codigoItem"].ToString()});
            }

            return listTemple;
        }
        [WebMethod]
        public List<Stocktaking> SelectLikeReference(string codBarras,string codUbicacion, int codProyecto)
        {
            List<Stocktaking> listTemple = new List<Stocktaking>();
            DataTable selectTable = new DataTable();

            Stocktaking RefrenceExist = new Stocktaking();
            selectTable = RefrenceExist.SelectLikeReference(codBarras, codUbicacion, codProyecto);

            foreach (DataRow row in selectTable.Rows)
            {
                listTemple.Add(new Stocktaking() { _Ubicacion = row["codigoUbicacion"].ToString(), _Proyectos = row["DescripcióniContruye"].ToString() ,_CodigoBarras = row["codigoBarras"].ToString(), _CodigoBarrasIcontruye = row["codigoBarrasIC"].ToString(), _DescripcionItem = row["descripcionItem"].ToString(), _Grupo = row["grupo"].ToString(), _Cantidad = Int32.Parse(row["cantidad"].ToString()), _Imagen = row["imagen"].ToString(), _Marcas = row["marca"].ToString(), _nombreGrupo = row["NombreGr"].ToString(), _Color = row["color"].ToString()});
            }

            return listTemple;
        }
        [WebMethod]

        public List<Stocktaking> getLocation(string codBarras)
        {
            List<Stocktaking> listTemple = new List<Stocktaking>();
            DataTable selectTable = new DataTable();

            Stocktaking locations = new Stocktaking();
            selectTable = locations.getLocation(codBarras);

            foreach (DataRow row in selectTable.Rows)
            {
                listTemple.Add(new Stocktaking() { _Ubicacion = row["codigoUbicacion"].ToString(), _Cantidad = Int32.Parse(row["cantidad"].ToString()) });
            }

            return listTemple;
            //select codigoUbicacion from NUbicacionXReferenciaCB where codigoBarras = + codBarras +
        }
        [WebMethod]
        public List<Stocktaking> SelectLikeReferenceCod(string codBarras)
        {
            List<Stocktaking> listTemple = new List<Stocktaking>();
            DataTable selectTable = new DataTable();

            Stocktaking RefrenceExist = new Stocktaking();
            selectTable = RefrenceExist.SelectLikeReferenceCod(codBarras);

            foreach (DataRow row in selectTable.Rows)
            {
                listTemple.Add(new Stocktaking() {_CodigoBarras = row["codigoBarras"].ToString(), _CodigoBarrasIcontruye = row["codigoBarrasIC"].ToString(), _DescripcionItem = row["descripcionItem"].ToString(), _Grupo = row["grupo"].ToString(), _Imagen = row["imagen"].ToString(), _Marcas = row["marca"].ToString(), _nombreGrupo = row["NombreGr"].ToString(), _Color = row["color"].ToString() });
            }

            return listTemple;
        }
        [WebMethod]
        public int InsertReference(string codigoUbicacion, string codigoBarras, string codigoBarrasIC, string descripcionItem, int id_Marca, int grupo, int cantidad, string imagen, string proyecto, string equivalencias, bool existe, string color)
        {
            int actividades;
            Stocktaking insertR = new Stocktaking();
            actividades = insertR.InsertReference(codigoUbicacion, codigoBarras, codigoBarrasIC, descripcionItem, id_Marca, grupo, cantidad, imagen, proyecto, equivalencias, existe, color);

            return actividades;
        }
        [WebMethod]
        public int Insertequivalences(string equivalencias, string codigoBarras)
        {
            int actividades;
            Stocktaking insertR = new Stocktaking();
            actividades = insertR.Insertequivalences(equivalencias, codigoBarras);

            return actividades;
        }
        [WebMethod]
        public int UpdateReference(string codigoUbicacion, string codigoBarras, string codigoBarrasIC, string descripcionItem, int id_Marca, int grupo, int cantidad, string imagen, string equivalencias,string color)
        {
            int actividades;
            Stocktaking UpdateR = new Stocktaking();
            actividades = UpdateR.UpdateReference(codigoUbicacion, codigoBarras, codigoBarrasIC, descripcionItem, id_Marca, grupo, cantidad, imagen, equivalencias, color);

            return actividades;
        }

    }
}
