using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Inventario
{
    public class Stocktaking
    {
        #region Atributos

        #region Marcas 
        private int id_Marcas;
        private string Marcas;
        #endregion

        #region Proyectos
        private string codProyectos;
        private string proyecto;
        #endregion

        #region Grupos
        private int clasificacion;
        private string nombreGrupo;
        #endregion

        #region Referencias 
        private string CodigoBarrasIcontruye;
        private string CodigoBarras;
        private string Ubicacion;
        private string Equivalencia;
        private int Cantidad;
        private string Grupo;
        private string Imagen;
        private string DescripcionItem;
        private string color;
        #endregion

        #endregion

        #region Metodos get y set
        public int _id_Marcas
        {
            get { return id_Marcas; }
            set { id_Marcas = value; }
        }

        public string _Marcas
        {
            get { return Marcas; }
            set { Marcas = value; }
        }
        public int _clasificacion
        {
            get { return clasificacion; }
            set { clasificacion = value; }
        }
        public string _nombreGrupo
        {
            get { return nombreGrupo; }
            set { nombreGrupo = value; }
        }
        public string _CodigoBarrasIcontruye
        {
            get { return CodigoBarrasIcontruye; }
            set { CodigoBarrasIcontruye = value; }
        }
        public string _CodigoBarras
        {
            get { return CodigoBarras; }
            set { CodigoBarras = value; }
        }
        public string _Ubicacion
        {
            get { return Ubicacion; }
            set { Ubicacion = value; }
        }
        public string _Equivalencia
        {
            get { return Equivalencia; }
            set { Equivalencia = value; }
        }
        public int _Cantidad
        {
            get { return Cantidad; }
            set { Cantidad = value; }
        }
        public string _Grupo
        {
            get { return Grupo; }
            set { Grupo = value; }
        }
        public string _DescripcionItem
        {
            get { return DescripcionItem; }
            set { DescripcionItem = value; }
        }
        public string _Imagen
        {
            get { return Imagen; }
            set { Imagen = value; }
        }
        public string _Color
        {
            get { return color; }
            set { color = value; }
        }
        public string _codProyectos
        {
            get { return codProyectos; }
            set { codProyectos = value; }
        }
        public string _Proyectos
        {
            get { return proyecto; }
            set { proyecto = value; }
        }
        #endregion

        #region Metodos CRUD
        //Trae todo el listado de Marcas
        public DataTable SelectMarca()
        {
            SqlCommand comando = ConexionBD.crearcomandoInventario();
            comando.CommandText = "select id, Marca FROM Marcas";
            return ConexionBD.EjecutarSelect(comando);
        }
        //Trae todo el listado de Grupos
        public DataTable SelectGrupo()
        {
            SqlCommand comando = ConexionBD.crearcomando();
            comando.CommandText = "select Clsif, NombreGr FROM Grupos";
            return ConexionBD.EjecutarSelect(comando);
        }
        //Valida si el codigo leido o ingresado de la referencia existe en icostruye
        public DataTable SelectLikeRIconstruye(string search, int condition)
        {
            SqlCommand comando = ConexionBD.crearcomando();

            if (condition == 1)
                comando.CommandText = "SELECT * FROM ( SELECT  m.CodigoItem,g.NombreGr,s.descripcionItem, m.Clasif, ROW_NUMBER() OVER(PARTITION BY m.codigoItem ORDER BY m.codigoItem DESC) rn FROM MaestroXGrupos m INNER JOIN Grupos g ON m.Clasif = g.Clsif INNER JOIN SeguimientoPMV2 s ON s.codigoItem = m.CodigoItem WHERE m.CodigoItem LIKE '%" + search + "%') a WHERE rn = 1";
            else if (condition == 2)
                comando.CommandText = "SELECT * FROM ( SELECT  m.CodigoItem,g.NombreGr,s.descripcionItem, ROW_NUMBER() OVER(PARTITION BY m.codigoItem ORDER BY m.codigoItem DESC) rn FROM MaestroXGrupos m INNER JOIN Grupos g ON m.Clasif = g.Clsif INNER JOIN SeguimientoPMV2 s ON s.codigoItem = m.CodigoItemWHERE s.descripcionItem LIKE '%" + search + "%') a WHERE rn = 1";

            return ConexionBD.EjecutarSelect(comando);
        }
        public DataTable SelectLikeRIconstruyeAll(string search, int condition)
        {
            SqlCommand comando = ConexionBD.crearcomando();

            if (condition == 1)
                comando.CommandText = "SELECT * FROM ( SELECT  m.CodigoItem,g.NombreGr,s.descripcion, m.Clasif, ROW_NUMBER() OVER(PARTITION BY m.codigoItem ORDER BY m.codigoItem DESC) rn FROM MaestroXGrupos m INNER JOIN Grupos g ON m.Clasif = g.Clsif INNER JOIN MxG2 s ON m.CodigoItem = s.CodigoItem WHERE m.CodigoItem LIKE '%" + search + "%') a WHERE rn = 1";
            else if (condition == 2)
                comando.CommandText = "SELECT * FROM ( SELECT  m.CodigoItem,g.NombreGr,s.descripcion, m.Clasif, ROW_NUMBER() OVER(PARTITION BY m.codigoItem ORDER BY m.codigoItem DESC) rn FROM MaestroXGrupos m INNER JOIN Grupos g ON m.Clasif = g.Clsif INNER JOIN MxG2 s ON m.CodigoItem = s.CodigoItem WHERE s.descripcionItem LIKE '%" + search + "%') a WHERE rn = 1";

            return ConexionBD.EjecutarSelect(comando);
        }
        //Valida si el codigo leido o ingresado de la ubicación existe en icostruye
        public DataTable SelectLikeLocationItem(string codBarrasLocation)
        {
            SqlCommand comando = ConexionBD.crearcomandoInventario();
            comando.CommandText = "select codigoUbicacion FROM Ubicacion WHERE codigoUbicacion = '" + codBarrasLocation + "'";
            return ConexionBD.EjecutarSelect(comando);
        }
        //Valida si el codigo leido o ingresado de la refencia del inventario actual existe
        public DataTable SelectLikeReference(string codBarras, string codUbicacion, int codProyecto)
        {
            SqlCommand comando = ConexionBD.crearcomandoInventario();
            int existRefeference = SelectLikeReferenceCod2(codBarras);
            if (existRefeference>0)
                comando.CommandText = "select re.codigoBarras, re.codigoBarrasIC, re.descripcionItem, re.grupo, re.color, nu.cantidad, re.imagen, m.marca, gr.NombreGr, nu.codigoUbicacion, ub.CodigoProyecto, eq.DescripcióniContruye FROM ReferenciasCodBarras re INNER JOIN Marcas m ON m.id = re.id_Marca INNER JOIN iConstruyeR.dbo.Grupos gr ON gr.Clsif = re.grupo INNER JOIN NUbicacionXReferenciaCB nu ON nu.CodigoBarras = re.codigoBarras INNER JOIN Ubicacion ub ON ub.codigoUbicacion = nu.codigoUbicacion INNER JOIN iConstruyeR.dbo.EquivCCostos eq ON ub.CodigoProyecto = eq.CodCCiConstruye WHERE re.codigoBarras = '" + codBarras + "' AND nu.codigoUbicacion = '" + codUbicacion + "' AND ub.CodigoProyecto = " + codProyecto + "";
            else
                comando.CommandText = "select re.codigoBarras, re.codigoBarrasIC, re.descripcionItem, re.grupo, re.color, nu.cantidad, re.imagen, m.marca, gr.NombreGr, nu.codigoUbicacion, ub.CodigoProyecto, eq.DescripcióniContruye FROM ReferenciasCodBarras re INNER JOIN Marcas m ON m.id = re.id_Marca INNER JOIN iConstruyeR.dbo.Grupos gr ON gr.Clsif = re.grupo INNER JOIN NUbicacionXReferenciaCB nu ON nu.CodigoBarras = re.codigoBarras INNER JOIN Ubicacion ub ON ub.codigoUbicacion = nu.codigoUbicacion INNER JOIN iConstruyeR.dbo.EquivCCostos eq ON ub.CodigoProyecto = eq.CodCCiConstruye INNER JOIN Equivalencias equ ON equ.codigoBarras = re.codigoBarras WHERE  nu.codigoUbicacion = '" + codUbicacion + "' AND ub.CodigoProyecto = " + codProyecto + " AND equ.codigoItem = '" + codBarras + "'";
            return ConexionBD.EjecutarSelect(comando);
        }
        public DataTable SelectLikeReferenceCod(string codBarras)
        {
            SqlCommand comando = ConexionBD.crearcomandoInventario();
            int existRefeference = SelectLikeReferenceCod2(codBarras);
            if (existRefeference > 0)
                 comando.CommandText = "select re.codigoBarras, re.codigoBarrasIC, re.descripcionItem, re.grupo, re.imagen, re.color, m.marca, gr.NombreGr FROM ReferenciasCodBarras re INNER JOIN Marcas m ON m.id = re.id_Marca INNER JOIN iConstruyeR.dbo.Grupos gr ON gr.Clsif = re.grupo WHERE re.codigoBarras = '" + codBarras + "'";
           
            else
                comando.CommandText = "select re.codigoBarras, re.codigoBarrasIC, re.descripcionItem, re.grupo, re.imagen, re.color, m.marca, gr.NombreGr FROM ReferenciasCodBarras re INNER JOIN Marcas m ON m.id = re.id_Marca INNER JOIN iConstruyeR.dbo.Grupos gr ON gr.Clsif = re.grupo INNER JOIN Equivalencias equ ON equ.codigoBarras = re.codigoBarras  WHERE equ.codigoItem = '" + codBarras + "'";
            return ConexionBD.EjecutarSelect(comando);
        }
        public int InsertReference(string codigoUbicacion, string codigoBarras, string codigoBarrasIC, string descripcionItem, int id_Marca, int grupo, int cantidad, string imagen, string proyecto, string equivalencias, bool existe, string color)
        {

            int existReference = SelectLikeReferenceCod2(codigoBarras);
            var equivalenciasSave = equivalencias.Split(',');
            var LengthqEquivalence = equivalenciasSave.Length;
            
            SqlCommand comando = ConexionBD.crearcomandoInventario();
            if (!existe)
            {
                if (equivalencias == null || equivalencias == "undefined" || equivalencias == "")
                {

                    if (existReference>0)
                    {
                        comando.CommandText = "INSERT INTO Ubicacion(codigoUbicacion,CodigoProyecto) VALUES('" + codigoUbicacion + "','" + proyecto + "') " +
                                              "INSERT INTO NUbicacionXReferenciaCB(codigoUbicacion,CodigoBarras,cantidad) VALUES('" + codigoUbicacion + "','" + codigoBarras + "'," + cantidad + ")";
                    }
                    else { 

                        comando.CommandText = "INSERT INTO Ubicacion(codigoUbicacion,CodigoProyecto) VALUES('" + codigoUbicacion + "','" + proyecto + "') " +
                                              "INSERT INTO ReferenciasCodBarras(codigoBarras,codigoBarrasIC,descripcionItem,id_Marca,grupo,imagen,color) VALUES('" + codigoBarras + "','" + codigoBarrasIC + "','" + descripcionItem + "'," + id_Marca + "," + grupo + ",'" + imagen + "', '" + color + "') " +
                                              "INSERT INTO NUbicacionXReferenciaCB(codigoUbicacion,CodigoBarras,cantidad) VALUES('" + codigoUbicacion + "','" + codigoBarras + "'," + cantidad + ")";
                   }
                }
                else
                {
                    if (existReference > 0)
                    {

                        comando.CommandText = "INSERT INTO Ubicacion(codigoUbicacion,CodigoProyecto) VALUES('" + codigoUbicacion + "','" + proyecto + "') " +
                                              "INSERT INTO NUbicacionXReferenciaCB(codigoUbicacion,CodigoBarras,cantidad) VALUES('" + codigoUbicacion + "','" + codigoBarras + "'," + cantidad + ")";

                        for (var i = 0; i < LengthqEquivalence; i++)
                        {
                            var equivalenceshort = equivalenciasSave[i].Split('.');
                            var equivalenceshortsend = equivalenceshort[equivalenceshort.Length-1];
                            int equivalenceInsert = LikeReferenceInsert(equivalenceshortsend);
                            int equivalenceIconstruye = LikeReferenceInsertIC(equivalenciasSave[i]);
                            if (equivalenceInsert <= 0 && equivalenceIconstruye <= 0)
                            { 
                                comando.CommandText = comando.CommandText + "INSERT INTO ReferenciasCodBarras VALUES('','" + equivalenceshortsend + "'," + 1 + ",'" + descripcionItem + "'," + grupo + ",'','verde')" +
                                                      "INSERT INTO NUbicacionXReferenciaCB(codigoUbicacion,CodigoBarras,cantidad) VALUES('" + codigoUbicacion + "','" + equivalenceshortsend + "'," + 0 + ")";
                            }

                            int existElength = ExistReference(codigoBarras, equivalenceshortsend);
                            if (existElength <= 0)
                                comando.CommandText = comando.CommandText + "INSERT INTO Equivalencias(codigoBarras,codigoItem) VALUES('" + codigoBarras + "','" + equivalenceshortsend + "')";
                        }
                    }
                    else
                    {
                        comando.CommandText = "INSERT INTO Ubicacion(codigoUbicacion,CodigoProyecto) VALUES('" + codigoUbicacion + "','" + proyecto + "') " +
                                              "INSERT INTO ReferenciasCodBarras(codigoBarras,codigoBarrasIC,descripcionItem,id_Marca,grupo,imagen,color) VALUES('" + codigoBarras + "','" + codigoBarrasIC + "','" + descripcionItem + "'," + id_Marca + "," + grupo + ",'" + imagen + "', '" + color + "') " +
                                              "INSERT INTO NUbicacionXReferenciaCB(codigoUbicacion,CodigoBarras,cantidad) VALUES('" + codigoUbicacion + "','" + codigoBarras + "'," + cantidad + ")";

                        for (var i=0;i<LengthqEquivalence;i++)
                        {
                            var equivalenceshort = equivalenciasSave[i].Split('.');
                            var equivalenceshortsend = equivalenceshort[equivalenceshort.Length - 1];
                            int equivalenceInsert = LikeReferenceInsert(equivalenceshortsend);
                            int equivalenceIconstruye = LikeReferenceInsertIC(equivalenciasSave[i]);
                            if (equivalenceInsert <= 0 && equivalenceIconstruye <= 0)
                            {
                                comando.CommandText = comando.CommandText + "INSERT INTO ReferenciasCodBarras VALUES('','" + equivalenceshortsend + "'," + 1 + ",'" + descripcionItem + "'," + grupo + ",'','verde')" +
                                                      "INSERT INTO NUbicacionXReferenciaCB(codigoUbicacion,CodigoBarras,cantidad) VALUES('" + codigoUbicacion + "','" + equivalenceshortsend + "'," + 0 + ")";
                            }

                            comando.CommandText = comando.CommandText + "INSERT INTO Equivalencias(codigoBarras,codigoItem) VALUES('" + codigoBarras + "','" + equivalenceshortsend + "')";
                        }

                    }

                }
    
            }
            else
            {
                if (equivalencias == null || equivalencias == "undefined" || equivalencias == "")
                {

                    if (existReference > 0)
                    { 
                        comando.CommandText = "INSERT INTO NUbicacionXReferenciaCB(codigoUbicacion,CodigoBarras,cantidad) VALUES('" + codigoUbicacion + "','" + codigoBarras + "'," + cantidad + ")";
                    }else
                        comando.CommandText = "INSERT INTO ReferenciasCodBarras(codigoBarras,codigoBarrasIC,descripcionItem,id_Marca,grupo,imagen,color) VALUES('" + codigoBarras + "','" + codigoBarrasIC + "','" + descripcionItem + "'," + id_Marca + "," + grupo + ",'" + imagen + "', '" + color + "') " +
                                              "INSERT INTO NUbicacionXReferenciaCB(codigoUbicacion,CodigoBarras,cantidad) VALUES('" + codigoUbicacion + "','" + codigoBarras + "'," + cantidad + ")";
                }
                else
                {
                    if (existReference > 0)
                    {

                        comando.CommandText = "INSERT INTO NUbicacionXReferenciaCB(codigoUbicacion,CodigoBarras,cantidad) VALUES('" + codigoUbicacion + "','" + codigoBarras + "'," + cantidad + ")";

                        for (var i = 0; i < LengthqEquivalence; i++)
                        {
                            var equivalenceshort = equivalenciasSave[i].Split('.');
                            var equivalenceshortsend = equivalenceshort[equivalenceshort.Length - 1];
                            int equivalenceInsert = LikeReferenceInsert(equivalenceshortsend);
                            int equivalenceIconstruye = LikeReferenceInsertIC(equivalenciasSave[i]);
                            if (equivalenceInsert <= 0 && equivalenceIconstruye <= 0)
                            {
                                comando.CommandText = comando.CommandText + "INSERT INTO ReferenciasCodBarras VALUES('','" + equivalenceshortsend + "'," + 1 + ",'" + descripcionItem + "'," + grupo + ",'','verde')" +
                                                      "INSERT INTO NUbicacionXReferenciaCB(codigoUbicacion,CodigoBarras,cantidad) VALUES('" + codigoUbicacion + "','" + equivalenceshortsend + "'," + 0 + ")";
                            }

                            int existElength = ExistReference(codigoBarras, equivalenceshortsend);
                            if (existElength <= 0)
                                comando.CommandText = comando.CommandText + "INSERT INTO Equivalencias(codigoBarras,codigoItem) VALUES('" + codigoBarras + "','" + equivalenceshortsend + "')";
                        }
                    }
                    else
                    {
                        comando.CommandText = "INSERT INTO ReferenciasCodBarras(codigoBarras,codigoBarrasIC,descripcionItem,id_Marca,grupo,imagen,color) VALUES('" + codigoBarras + "','" + codigoBarrasIC + "','" + descripcionItem + "'," + id_Marca + "," + grupo + ",'" + imagen + "', '" + color + "') " +
                                              "INSERT INTO NUbicacionXReferenciaCB(codigoUbicacion,CodigoBarras,cantidad) VALUES('" + codigoUbicacion + "','" + codigoBarras + "'," + cantidad + ")";

                        for (var i = 0; i < LengthqEquivalence; i++)
                        {
                            var equivalenceshort = equivalenciasSave[i].Split('.');
                            var equivalenceshortsend = equivalenceshort[equivalenceshort.Length - 1];
                            int equivalenceInsert = LikeReferenceInsert(equivalenceshortsend);
                            int equivalenceIconstruye = LikeReferenceInsertIC(equivalenciasSave[i]);
                            if (equivalenceInsert <= 0 && equivalenceIconstruye <= 0)
                            {
                                comando.CommandText = comando.CommandText + "INSERT INTO ReferenciasCodBarras VALUES('','" + equivalenceshortsend + "'," + 1 + ",'" + descripcionItem + "'," + grupo + ",'','verde')" +
                                                      "INSERT INTO NUbicacionXReferenciaCB(codigoUbicacion,CodigoBarras,cantidad) VALUES('" + codigoUbicacion + "','" + equivalenceshortsend + "'," + 0 + ")";
                            }

                            comando.CommandText = comando.CommandText + "INSERT INTO Equivalencias(codigoBarras,codigoItem) VALUES('" + codigoBarras + "','" + equivalenceshortsend + "')";
                        }
                    }
                }
            }
            return ConexionBD.EjecutarComando(comando);
        }
        public int Insertequivalences(string equivalencias, string codigoBarras)
        {
            SqlCommand comando = ConexionBD.crearcomandoInventario();
            comando.CommandText = "INSERT INTO Equivalencias(codigoBarras,codigoItem) VALUES('" + codigoBarras + "','" + equivalencias + "')";
            return ConexionBD.EjecutarComando(comando);
        }
        public int UpdateReference(string codigoUbicacion, string codigoBarras, string codigoBarrasIC, string descripcionItem, int id_Marca, int grupo, int cantidad, string imagen, string equivalencias, string color)
        {

            var equivalenciasSave = equivalencias.Split(',');
            var LengthqEquivalence = equivalenciasSave.Length;
            SqlCommand comando = ConexionBD.crearcomandoInventario();
            comando.CommandText = "UPDATE ReferenciasCodBarras SET codigoBarrasIC = '" + codigoBarrasIC + "', id_Marca = " + id_Marca + ", descripcionItem = '" + descripcionItem + "', grupo = " + grupo + ", imagen = '" + imagen + "', color = '" + color + "' WHERE codigoBarras = '" + codigoBarras + "'" +
                                  "UPDATE NUbicacionXReferenciaCB SET codigoUbicacion = '" + codigoUbicacion + "', cantidad = " + cantidad + " WHERE CodigoBarras = '" + codigoBarras + "'";

            if (equivalencias != "")
            {
                for (var i = 0; i < LengthqEquivalence; i++)
                {
                    var equivalenceshort = equivalenciasSave[i].Split('.');
                    var equivalenceshortsend = equivalenceshort[equivalenceshort.Length - 1];
                    int equivalenceInsert = LikeReferenceInsert(equivalenceshortsend);
                    int codeSplit = LikeReferenceInsertICSplit(equivalenciasSave[i]);
                    if (equivalenceInsert <= 0 && codeSplit <= 0)
                    {
                        comando.CommandText = comando.CommandText + "INSERT INTO ReferenciasCodBarras VALUES('','" + equivalenceshortsend + "'," + 1 + ",'" + descripcionItem + "'," + grupo + ",'','verde')" +
                                              "INSERT INTO NUbicacionXReferenciaCB(codigoUbicacion,CodigoBarras,cantidad) VALUES('" + codigoUbicacion + "','" + equivalenceshortsend + "'," + 0 + ")";
                    }

                    int existElength = ExistReference(codigoBarras, equivalenceshortsend);
                    if (existElength <= 0)
                        comando.CommandText = comando.CommandText + "INSERT INTO Equivalencias(codigoBarras,codigoItem) VALUES('" + codigoBarras + "','" + equivalenceshortsend + "')";
                }
            }
            return ConexionBD.EjecutarComando(comando);
        }

        public DataTable getLocation(string codBarras)
        {
            SqlCommand comando = ConexionBD.crearcomandoInventario();
            comando.CommandText = "select codigoUbicacion,cantidad from NUbicacionXReferenciaCB where codigoBarras = '" + codBarras + "'";
            return ConexionBD.EjecutarSelect(comando);
        }

        public DataTable selectProyectos()
        {
            SqlCommand comando = ConexionBD.crearcomando();
            comando.CommandText = "select CodCCiConstruye, DescripcióniContruye from EquivCCostos";
            return ConexionBD.EjecutarSelect(comando);
        }
        public int SelectLikeReferenceCod2(string codBarras)
        {
            DataTable consult;
            int returnLength = 0;
            SqlCommand comando = ConexionBD.crearcomandoInventario();
            comando.CommandText = "select re.codigoBarras, re.codigoBarrasIC, re.descripcionItem, re.grupo, re.imagen, m.marca, gr.NombreGr FROM ReferenciasCodBarras re INNER JOIN Marcas m ON m.id = re.id_Marca INNER JOIN iConstruyeR.dbo.Grupos gr ON gr.Clsif = re.grupo WHERE re.codigoBarras = '" + codBarras + "'";
            consult = ConexionBD.EjecutarSelect(comando);
            returnLength = consult.Rows.Count;
            return returnLength;
        }
        public int LikeReferenceInsert(string codBarras)
        {
            DataTable consult;
            int returnLength = 0;
            SqlCommand comando = ConexionBD.crearcomandoInventario();
            comando.CommandText = "select re.codigoBarras FROM ReferenciasCodBarras re WHERE re.codigoBarras = '" + codBarras + "'";
            consult = ConexionBD.EjecutarSelect(comando);
            returnLength = consult.Rows.Count;
            return returnLength;
        }
        public int LikeReferenceInsertIC(string codBarras)
        {
            DataTable consult;
            int returnLength = 0;
            SqlCommand comando = ConexionBD.crearcomando();
            comando.CommandText = "select m.CodigoItem FROM MaestroXGrupos m WHERE m.CodigoItem = '" + codBarras + "'";
            consult = ConexionBD.EjecutarSelect(comando);
            returnLength = consult.Rows.Count;
            return returnLength;
        }
        public int LikeReferenceInsertICSplit(string codBarras)
        {
            DataTable consult;
            int existReference = 0;
            SqlCommand comando = ConexionBD.crearcomando();
            comando.CommandText = "select m.CodigoItem FROM MaestroXGrupos m WHERE m.CodigoItem LIKE '%" + codBarras.Trim() + "%'";
            consult = ConexionBD.EjecutarSelect(comando);

            foreach (DataRow row in consult.Rows)
            {
                string codigoItem = row["CodigoItem"].ToString();
                var codigoItemSplit = codigoItem.Split('.');
                codigoItem = codigoItemSplit[codigoItemSplit.Length - 1];
                if (codigoItem == codBarras.Trim())
                {
                    existReference = 1;
                    break;
                }

            }

            return existReference;
        }
        public DataTable selectEquivalences(string codBarras)
        {
            SqlCommand comando = ConexionBD.crearcomandoInventario();
            comando.CommandText = "select codigoItem from Equivalencias WHERE codigoBarras = '" + codBarras + "'";
            return ConexionBD.EjecutarSelect(comando);
        }
        public int ExistReference(string codBarras, string equivalencia)
        {
            DataTable consult;
            int returnLength = 0;
            SqlCommand comando = ConexionBD.crearcomandoInventario();
            comando.CommandText = "select codigoBarras, codigoItem from Equivalencias  WHERE codigoBarras = '" + codBarras + "' AND codigoItem = '" + equivalencia + "' ";
            consult = ConexionBD.EjecutarSelect(comando);
            returnLength = consult.Rows.Count;
            return returnLength;
        }
        public int SelectLikeReferenceCodxUbicacion(string codBarras, string ubicacion)
        {
            DataTable consult;
            int returnLength = 0;
            SqlCommand comando = ConexionBD.crearcomandoInventario();
            comando.CommandText = "select n.codigoBarras, n.codigoUbicacion FROM NUbicacionXReferenciaCB n WHERE codigoBarras = '" + codBarras + "' AND codigoUbicacion = '" + ubicacion + "'";
            consult = ConexionBD.EjecutarSelect(comando);
            returnLength = consult.Rows.Count;
            return returnLength;
        }

        public Stocktaking() { }
    }

}
#endregion