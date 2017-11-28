#region 
/***********************************************************************************************************
	NOMBRE:       RnPosTulante
	DESCRIPCION:
		Clase que implmenta los metodos y operaciones sobre la Tabla postulante

	REVISIONES:
		Ver        FECHA       Autor            Descripcion 
		---------  ----------  ---------------  ------------------------------------
		1.0        28/11/2017  Ivan Cruz        Creacion 

*************************************************************************************************************/
#endregion



#region
using System;
using System.Globalization;
using System.Threading;

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using Autoescuelas.Dal; 
using Autoescuelas.PgConn; 
using Autoescuelas.Dal.Entidades;
using System.Web.UI.WebControls;
#endregion

namespace Autoescuelas.Dal.Modelo
{
	public class RnPosTulante
	{
		#region Reflection
        
		/// <summary>
		/// Metodo para castear Dinamicamente un Tipo
		/// </summary>
		/// <param name="valor">Tipo a ser casteado</param>
		/// <param name="myField">Enum de la columna</param>
		/// <returns>Devuelve un objeto del Tipo de la columna especificada en el Enum</returns>
		public object GetColumnType(object valor, EntPosTulante.Fields myField)
		{
			if (DBNull.Value.Equals(valor)) 
				return null;
			Type destino = typeof(EntPosTulante).GetProperty(myField.ToString()).PropertyType;
			var miTipo = Nullable.GetUnderlyingType(destino) ?? destino;
			
			try
			{
				TypeConverter tc = TypeDescriptor.GetConverter(miTipo);
				return tc.ConvertFrom(valor);
			}
			catch (Exception)
			{
				return Convert.ChangeType(valor, miTipo);
			}
		}

		/// <summary>
		/// Metodo para castear Dinamicamente un Tipo
		/// </summary>
		/// <param name="valor">Tipo a ser casteado</param>
		/// <param name="strField">Nombre de la columna</param>
		/// <returns>Devuelve un objeto del Tipo de la columna especificada en el Enum</returns>
		public object GetColumnType(object valor, string strField)
		{
			if (DBNull.Value.Equals(valor)) 
				return null;
			Type destino = typeof(EntPosTulante).GetProperty(strField).PropertyType;
			var miTipo = Nullable.GetUnderlyingType(destino) ?? destino;
			
			try
			{
				TypeConverter tc = TypeDescriptor.GetConverter(miTipo);
				return tc.ConvertFrom(valor);
			}
			catch (Exception)
			{
				return Convert.ChangeType(valor, miTipo);
			}
		}
        
		/// <summary>
		/// 	 Funcion que obtiene la llave primaria unica de la tabla postulante a partir de una cadena
		/// </summary>
		/// <param name="args" type="string[]">
		///     <para>
		/// 		 Cadena desde la que se construye el identificador unico de la tabla postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Identificador unico de la tabla postulante
		/// </returns>
		public string CreatePk(string[] args)
		{
			return args[0];
		}
		
		#endregion 

		#region ObtenerObjeto

		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntPosTulante a partir de la llave primaria
		/// </summary>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public EntPosTulante ObtenerObjeto(int intid_postulante)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(EntPosTulante.Fields.id_postulante.ToString());
		
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(intid_postulante);
			
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntPosTulante a partir del usuario que inserta
		/// </summary>
		/// <param name="strUsuCre">Login o nombre de usuario</param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public EntPosTulante ObtenerObjetoInsertado(string strUsuCre)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(EntPosTulante.Fields.usucre.ToString());
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'" + strUsuCre + "'");
			
			int iIdInsertado = FuncionesMax(EntPosTulante.Fields.id_postulante, arrColumnasWhere, arrValoresWhere);
			
			return ObtenerObjeto(iIdInsertado);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public EntPosTulante ObtenerObjeto(ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public EntPosTulante ObtenerObjeto(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntPosTulante.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count == 1)
				{
					EntPosTulante obj = new EntPosTulante();
					obj = crearObjeto(table.Rows[0]);
					return obj;
				}
				else if (table.Rows.Count > 1)
					throw new Exception("Se ha devuelto mas de un objeto");
				else
					return null;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public EntPosTulante ObtenerObjeto(Hashtable htbFiltro)
		{
			return ObtenerObjeto(htbFiltro, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public EntPosTulante ObtenerObjeto(Hashtable htbFiltro, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
				
				ArrayList arrColumnasWhere = new ArrayList();
				ArrayList arrValoresWhere = new ArrayList();
				
				foreach (DictionaryEntry entry in htbFiltro)
				{
					arrColumnasWhere.Add(entry.Key.ToString());
					arrValoresWhere.Add(entry.Value.ToString());
				}
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntPosTulante.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count == 1)
				{
					EntPosTulante obj = new EntPosTulante();
					obj = crearObjeto(table.Rows[0]);
					return obj;
				}
				else if (table.Rows.Count > 1)
					throw new Exception("Se ha devuelto mas de un objeto");
				else
					return null;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public EntPosTulante ObtenerObjeto(EntPosTulante.Fields searchField, object searchValue)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public EntPosTulante ObtenerObjeto(EntPosTulante.Fields searchField, object searchValue, string strParamAdicionales)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere, strParamAdicionales);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un Business Object del Tipo EntPosTulante a partir de su llave promaria
		/// </summary>
		/// <returns>
		/// 	Objeto del Tipo EntPosTulante
		/// </returns>
		public EntPosTulante ObtenerObjeto(int intid_postulante, ref CTrans localTrans )
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(EntPosTulante.Fields.id_postulante.ToString());
		
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(intid_postulante);
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public EntPosTulante ObtenerObjeto(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans localTrans)
		{
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public EntPosTulante ObtenerObjeto(Hashtable htbFiltro, ref CTrans localTrans)
		{
			return ObtenerObjeto(htbFiltro, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public EntPosTulante ObtenerObjeto(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales,  ref CTrans localTrans)
		{
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public EntPosTulante ObtenerObjeto(Hashtable htbFiltro, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
				
				ArrayList arrColumnasWhere = new ArrayList();
				ArrayList arrValoresWhere = new ArrayList();
				
				foreach (DictionaryEntry entry in htbFiltro)
				{
					arrColumnasWhere.Add(entry.Key.ToString());
					arrValoresWhere.Add(entry.Value.ToString());
				}
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntPosTulante.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count == 1)
				{
					EntPosTulante obj = new EntPosTulante();
					obj = crearObjeto(table.Rows[0]);
					return obj;
				}
				else if (table.Rows.Count > 1)
					throw new Exception("Se ha devuelto mas de un objeto");
				else
					return null;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public EntPosTulante ObtenerObjeto(EntPosTulante.Fields searchField, object searchValue, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public EntPosTulante ObtenerObjeto(EntPosTulante.Fields searchField, object searchValue, string strParamAdicionales, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
		}
		

		#endregion

		#region ObtenerLista

		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerLista()
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerLista(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerLista(ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			return ObtenerLista(arrColumnasWhere, arrValoresWhere, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerLista(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntPosTulante.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearLista(table);
				}
				else
					return new List<EntPosTulante>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerLista(EntPosTulante.Fields searchField, object searchValue)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerLista(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerLista(EntPosTulante.Fields searchField, object searchValue, string strParamAdicionales)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerLista(arrColumnasWhere, arrValoresWhere, strParamAdicionales);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerLista(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans localTrans)
		{
			return ObtenerLista(arrColumnasWhere, arrValoresWhere, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerLista(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntPosTulante.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count > 0)
				{
					return crearLista(table);
				}
				else
					return new List<EntPosTulante>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerLista(EntPosTulante.Fields searchField, object searchValue, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerLista(arrColumnasWhere, arrValoresWhere, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerLista(EntPosTulante.Fields searchField, object searchValue, string strParamAdicionales, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerLista(arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerLista(Hashtable htbFiltro)
		{
			return ObtenerLista(htbFiltro, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerLista(Hashtable htbFiltro, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
				
				ArrayList arrColumnasWhere = new ArrayList();
				ArrayList arrValoresWhere = new ArrayList();
				
				foreach (DictionaryEntry entry in htbFiltro)
				{
					arrColumnasWhere.Add(entry.Key.ToString());
					arrValoresWhere.Add(entry.Value.ToString());
				}
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntPosTulante.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearLista(table);
				}
				else
					return new List<EntPosTulante>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerLista(Hashtable htbFiltro, ref CTrans localTrans)
		{
			return ObtenerLista(htbFiltro, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerLista(Hashtable htbFiltro, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
				
				ArrayList arrColumnasWhere = new ArrayList();
				ArrayList arrValoresWhere = new ArrayList();
				
				foreach (DictionaryEntry entry in htbFiltro)
				{
					arrColumnasWhere.Add(entry.Key.ToString());
					arrValoresWhere.Add(entry.Value.ToString());
				}
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntPosTulante.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count > 0)
				{
					return crearLista(table);
				}
				else
					return new List<EntPosTulante>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerListaDesdeVista(String strVista)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerListaDesdeVista(strVista, arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerListaDesdeVista(String strVista, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			return ObtenerListaDesdeVista(strVista, arrColumnasWhere, arrValoresWhere, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerListaDesdeVista(String strVista, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add("*");
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + strVista, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearListaRevisada(table);
				}
				else
					return new List<EntPosTulante>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerListaDesdeVista(String strVista, Hashtable htbFiltro)
		{
			return ObtenerListaDesdeVista(strVista, htbFiltro, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerListaDesdeVista(String strVista, Hashtable htbFiltro, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add("*");
				
				ArrayList arrColumnasWhere = new ArrayList();
				ArrayList arrValoresWhere = new ArrayList();
				
				foreach (DictionaryEntry entry in htbFiltro)
				{
					arrColumnasWhere.Add(entry.Key.ToString());
					arrValoresWhere.Add(entry.Value.ToString());
				}
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + strVista, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearListaRevisada(table);
				}
				else
					return new List<EntPosTulante>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerListaDesdeVista(String strVista, EntPosTulante.Fields searchField, object searchValue)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerListaDesdeVista(strVista, arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerListaDesdeVista(String strVista, EntPosTulante.Fields searchField, object searchValue, string strParamAdicionales)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerListaDesdeVista(strVista, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerListaDesdeVista(String strVista, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans localTrans)
		{
			return ObtenerListaDesdeVista(strVista, arrColumnasWhere, arrValoresWhere, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerListaDesdeVista(String strVista, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add("*");
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + strVista, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count > 0)
				{
					return crearListaRevisada(table);
				}
				else
					return new List<EntPosTulante>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerListaDesdeVista(String strVista, Hashtable htbFiltro, ref CTrans localTrans)
		{
			return ObtenerListaDesdeVista(strVista, htbFiltro, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerListaDesdeVista(String strVista, Hashtable htbFiltro, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add("*");
				
				ArrayList arrColumnasWhere = new ArrayList();
				ArrayList arrValoresWhere = new ArrayList();
				
				foreach (DictionaryEntry entry in htbFiltro)
				{
					arrColumnasWhere.Add(entry.Key.ToString());
					arrValoresWhere.Add(entry.Value.ToString());
				}
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + strVista, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count > 0)
				{
					return crearListaRevisada(table);
				}
				else
					return new List<EntPosTulante>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerListaDesdeVista(String strVista, EntPosTulante.Fields searchField, object searchValue, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerListaDesdeVista(strVista, arrColumnasWhere, arrValoresWhere, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntPosTulante> ObtenerListaDesdeVista(String strVista, EntPosTulante.Fields searchField, object searchValue, string strParamAdicionales, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerListaDesdeVista(strVista, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
		}
		

		#endregion 

		#region ObtenerCola y Obtener Pila

		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Queue que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntPosTulante> ObtenerCola()
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerCola(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Queue que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntPosTulante> ObtenerCola(ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			return ObtenerCola(arrColumnasWhere, arrValoresWhere, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Queue que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntPosTulante> ObtenerCola(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntPosTulante.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearCola(table);
				}
				else
					return new Queue<EntPosTulante>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Queue que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntPosTulante> ObtenerCola(EntPosTulante.Fields searchField, object searchValue)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerCola(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Queue que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntPosTulante> ObtenerCola(EntPosTulante.Fields searchField, object searchValue, string strParamAdicionales)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerCola(arrColumnasWhere, arrValoresWhere, strParamAdicionales);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Queue que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntPosTulante> ObtenerCola(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans localTrans)
		{
			return ObtenerCola(arrColumnasWhere, arrValoresWhere, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Queue que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntPosTulante> ObtenerCola(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntPosTulante.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count > 0)
				{
					return crearCola(table);
				}
				else
					return new Queue<EntPosTulante>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Queue que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntPosTulante> ObtenerCola(EntPosTulante.Fields searchField, object searchValue, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerCola(arrColumnasWhere, arrValoresWhere, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Stack que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntPosTulante> ObtenerCola(EntPosTulante.Fields searchField, object searchValue, string strParamAdicionales, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerCola(arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntPosTulante> ObtenerPila()
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerPila(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntPosTulante> ObtenerPila(ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			return ObtenerPila(arrColumnasWhere, arrValoresWhere, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntPosTulante> ObtenerPila(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntPosTulante.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearPila(table);
				}
				else
					return new Stack<EntPosTulante>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntPosTulante> ObtenerPila(EntPosTulante.Fields searchField, object searchValue)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerPila(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntPosTulante> ObtenerPila(EntPosTulante.Fields searchField, object searchValue, string strParamAdicionales)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerPila(arrColumnasWhere, arrValoresWhere, strParamAdicionales);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntPosTulante> ObtenerPila(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans localTrans)
		{
			return ObtenerPila(arrColumnasWhere, arrValoresWhere, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntPosTulante> ObtenerPila(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntPosTulante.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count > 0)
				{
					return crearPila(table);
				}
				else
					return new Stack<EntPosTulante>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntPosTulante> ObtenerPila(EntPosTulante.Fields searchField, object searchValue, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerPila(arrColumnasWhere, arrValoresWhere, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntPosTulante> ObtenerPila(EntPosTulante.Fields searchField, object searchValue, string strParamAdicionales, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerPila(arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
		}
		

		#endregion 

		#region ObtenerDataTable

		/// <summary>
		/// 	 Funcion que llena un DataTable con los registros de una tabla postulante
		/// </summary>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de postulante
		/// </returns>
		public DataTable NuevoDataTable()
		{
			try
			{
				DataTable table = new DataTable ();
				DataColumn dc;
				dc = new DataColumn(EntPosTulante.Fields.id_postulante.ToString(),typeof(EntPosTulante).GetProperty(EntPosTulante.Fields.id_postulante.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntPosTulante.Fields.ci_postulante.ToString(),typeof(EntPosTulante).GetProperty(EntPosTulante.Fields.ci_postulante.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntPosTulante.Fields.nombre_postulante.ToString(),typeof(EntPosTulante).GetProperty(EntPosTulante.Fields.nombre_postulante.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntPosTulante.Fields.ap_mat_postulante.ToString(),typeof(EntPosTulante).GetProperty(EntPosTulante.Fields.ap_mat_postulante.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntPosTulante.Fields.ap_pat_postulante.ToString(),typeof(EntPosTulante).GetProperty(EntPosTulante.Fields.ap_pat_postulante.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntPosTulante.Fields.celular_postulante.ToString(),typeof(EntPosTulante).GetProperty(EntPosTulante.Fields.celular_postulante.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntPosTulante.Fields.fecha_nac_postulante.ToString(),typeof(EntPosTulante).GetProperty(EntPosTulante.Fields.fecha_nac_postulante.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntPosTulante.Fields.apiestado.ToString(),typeof(EntPosTulante).GetProperty(EntPosTulante.Fields.apiestado.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntPosTulante.Fields.usucre.ToString(),typeof(EntPosTulante).GetProperty(EntPosTulante.Fields.usucre.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntPosTulante.Fields.feccre.ToString(),typeof(EntPosTulante).GetProperty(EntPosTulante.Fields.feccre.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntPosTulante.Fields.usumod.ToString(),typeof(EntPosTulante).GetProperty(EntPosTulante.Fields.usumod.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntPosTulante.Fields.fecmod.ToString(),typeof(EntPosTulante).GetProperty(EntPosTulante.Fields.fecmod.ToString()).PropertyType);
				table.Columns.Add(dc);

				
				return table;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que genera un DataTable con determinadas columnas de una postulante
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de postulante
		/// </returns>
		public DataTable NuevoDataTable(ArrayList arrColumnas)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add("'1'");
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add("'2'");
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntPosTulante.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere);
				
				return table;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registros de una tabla postulante
		/// </summary>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de postulante
		/// </returns>
		public DataTable ObtenerDataTable()
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerDataTable(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registros de una tabla y n condicion WHERE postulante
		/// </summary>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de postulante
		/// </returns>
		public DataTable ObtenerDataTable(String strParamAdicionales)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerDataTable(arrColumnasWhere, arrValoresWhere, strParamAdicionales);
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla postulante
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de postulante
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnas)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add("'1'");
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add("'1'");
				
				return ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla postulante
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="strParametrosAdicionales" type="string">
		///     <para>
		/// 		 Parametros adicionales
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de postulante
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnas, string strParametrosAdicionales)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add("'1'");
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add("'1'");
				
				return ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla postulante
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de postulante
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				return ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla postulante
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de postulante
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnas, Hashtable htbFiltro)
		{
			try
			{
				return ObtenerDataTable(arrColumnas, htbFiltro, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla postulante
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de postulante
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				return ObtenerDataTable(arrColumnasWhere, arrValoresWhere, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla postulante
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de postulante
		/// </returns>
		public DataTable ObtenerDataTable(Hashtable htbFiltro)
		{
			try
			{
				return ObtenerDataTable(htbFiltro, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla postulante
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParametrosAdicionales" type="string">
		///     <para>
		/// 		 Parametros adicionales
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de postulante
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParametrosAdicionales)
		{
			try
			{
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntPosTulante.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
				
				return table;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla postulante
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParametrosAdicionales" type="string">
		///     <para>
		/// 		 Parametros adicionales
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de postulante
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnas, Hashtable htbFiltro, string strParametrosAdicionales)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				ArrayList arrValoresWhere = new ArrayList();
				
				foreach (DictionaryEntry entry in htbFiltro)
				{
					arrColumnasWhere.Add(entry.Key.ToString());
					arrValoresWhere.Add(entry.Value.ToString());
				}
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntPosTulante.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
				
				return table;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla postulante
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParametrosAdicionales" type="string">
		///     <para>
		/// 		 Parametros adicionales
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de postulante
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParametrosAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
				
				return ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla postulante
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParametrosAdicionales" type="string">
		///     <para>
		/// 		 Parametros adicionales
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de postulante
		/// </returns>
		public DataTable ObtenerDataTable(Hashtable htbFiltro, string strParametrosAdicionales)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				ArrayList arrValoresWhere = new ArrayList();
				
				foreach (DictionaryEntry entry in htbFiltro)
				{
					arrColumnasWhere.Add(entry.Key.ToString());
					arrValoresWhere.Add(entry.Value.ToString());
				}
				
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
				
				return ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla postulante
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de postulante
		/// </returns>
		public DataTable ObtenerDataTableOr(ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
				return ObtenerDataTableOr(arrColumnas, arrColumnasWhere, arrValoresWhere, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla postulante
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de postulante
		/// </returns>
		public DataTable ObtenerDataTableOr(ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				return ObtenerDataTableOr(arrColumnas, arrColumnasWhere, arrValoresWhere, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla postulante
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParametrosAdicionales" type="string">
		///     <para>
		/// 		 Parametros adicionales
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de postulante
		/// </returns>
		public DataTable ObtenerDataTableOr(ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParametrosAdicionales)
		{
			try
			{
				CConn local = new CConn();
				DataTable table = local.cargarDataTableOr(CParametros.schema + EntPosTulante.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
				
				return table;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos a partir de un filtro realizado por algun campo
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <returns>
		/// 	DataTable que cumple con los filtros de los parametros
		/// </returns>
		public DataTable ObtenerDataTable(EntPosTulante.Fields searchField, object searchValue)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				
				return ObtenerDataTable(arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos a partir de un filtro realizado por algun campo
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParametrosAdicionales" type="string">
		///     <para>
		/// 		 Parametros adicionales
		///     </para>
		/// </param>
		/// <returns>
		/// 	DataTable que cumple con los filtros de los parametros
		/// </returns>
		public DataTable ObtenerDataTable(EntPosTulante.Fields searchField, object searchValue, string strParametrosAdicionales)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				
				return ObtenerDataTable(arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos a partir de un filtro realizado por algun campo
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParametrosAdicionales" type="string">
		///     <para>
		/// 		 Parametros adicionales
		///     </para>
		/// </param>
		/// <returns>
		/// 	DataTable que cumple con los filtros de los parametros
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnas, EntPosTulante.Fields searchField, object searchValue, string strParametrosAdicionales)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				
				return ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos a partir de un filtro realizado por algun campo
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <returns>
		/// 	DataTable que cumple con los filtros de los parametros
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnas, EntPosTulante.Fields searchField, object searchValue)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				
				return ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		

		#endregion 

		#region ObtenerDiccionario

		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntPosTulante> ObtenerDiccionario()
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerDiccionario(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntPosTulante> ObtenerDiccionario(ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			return ObtenerDiccionario(arrColumnasWhere, arrValoresWhere, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntPosTulante> ObtenerDiccionario(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntPosTulante.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearDiccionario(table);
				}
				else
					return new Dictionary<string, EntPosTulante>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntPosTulante> ObtenerDiccionario(EntPosTulante.Fields searchField, object searchValue)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerDiccionario(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntPosTulante> ObtenerDiccionario(EntPosTulante.Fields searchField, object searchValue, string strParamAdicionales)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerDiccionario(arrColumnasWhere, arrValoresWhere, strParamAdicionales);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntPosTulante> ObtenerDiccionario(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans localTrans)
		{
			return ObtenerDiccionario(arrColumnasWhere, arrValoresWhere, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntPosTulante> ObtenerDiccionario(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntPosTulante.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count > 0)
				{
					return crearDiccionario(table);
				}
				else
					return new Dictionary<string, EntPosTulante>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntPosTulante> ObtenerDiccionario(EntPosTulante.Fields searchField, object searchValue, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerDiccionario(arrColumnasWhere, arrValoresWhere, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntPosTulante a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntPosTulante> ObtenerDiccionario(EntPosTulante.Fields searchField, object searchValue, string strParamAdicionales, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerDiccionario(arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
		}
		
		public Dictionary<String, EntPosTulante> ObtenerDiccionarioKey(EntPosTulante.Fields dicKey)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerDiccionarioKey(arrColumnasWhere, arrValoresWhere, dicKey);
		}
		
		public Dictionary<String, EntPosTulante> ObtenerDiccionarioKey(String strParamAdic, EntPosTulante.Fields dicKey)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerDiccionarioKey(arrColumnasWhere, arrValoresWhere, strParamAdic, dicKey);
		}
		
		public Dictionary<String, EntPosTulante> ObtenerDiccionarioKey(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, EntPosTulante.Fields dicKey)
		{
			return ObtenerDiccionarioKey(arrColumnasWhere, arrValoresWhere, "", dicKey);
		}
		
		public Dictionary<String, EntPosTulante> ObtenerDiccionarioKey(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales, EntPosTulante.Fields dicKey)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntPosTulante.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearDiccionario(table, dicKey);
				}
				else
					return new Dictionary<string, EntPosTulante>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		public Dictionary<String, EntPosTulante> ObtenerDiccionarioKey(EntPosTulante.Fields searchField, object searchValue, EntPosTulante.Fields dicKey)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerDiccionarioKey(arrColumnasWhere, arrValoresWhere, dicKey);
		}
		
		public Dictionary<String, EntPosTulante> ObtenerDiccionarioKey(EntPosTulante.Fields searchField, object searchValue, string strParamAdicionales, EntPosTulante.Fields dicKey)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerDiccionarioKey(arrColumnasWhere, arrValoresWhere, strParamAdicionales, dicKey);
		}
		

		#endregion 

		#region ObjetoASp

		/// <summary>
		/// 	Funcion que inserta un nuevo registro en la tabla postulante a partir de una clase del tipo Epostulante
		/// </summary>
		/// <param name="strNombreSp" type="System.string">
		///     <para>
		/// 		 Nombre del Procedimiento a ejecutar sobre el SP
		///     </para>
		/// </param>
		/// <param name="obj" type="Entidades.EntPosTulante">
		///     <para>
		/// 		 Clase desde la que se va a ejecutar el SP de la tabla postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor de registros afectados en el Procedimiento de la tabla postulante
		/// </returns>
		public int EjecutarSpDesdeObjeto(string strNombreSp, EntPosTulante obj)
		{
			try
			{
				
				ArrayList arrNombreParam = new ArrayList();
				arrNombreParam.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.apiestado.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.usucre.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.feccre.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.usumod.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.fecmod.ToString());
				
				ArrayList arrValoresParam = new ArrayList();
				arrValoresParam.Add(obj.id_postulante);
				arrValoresParam.Add(obj.ci_postulante);
				arrValoresParam.Add(obj.nombre_postulante == null ? null : "'" + obj.nombre_postulante + "'");
				arrValoresParam.Add(obj.ap_mat_postulante == null ? null : "'" + obj.ap_mat_postulante + "'");
				arrValoresParam.Add(obj.ap_pat_postulante == null ? null : "'" + obj.ap_pat_postulante + "'");
				arrValoresParam.Add(obj.celular_postulante);
				arrValoresParam.Add(obj.fecha_nac_postulante == null ? null : "'" + Convert.ToDateTime(obj.fecha_nac_postulante).ToString(CParametros.ParFormatoFechaHora) + "'");
				arrValoresParam.Add(obj.apiestado == null ? null : "'" + obj.apiestado + "'");
				arrValoresParam.Add(obj.usucre == null ? null : "'" + obj.usucre + "'");
				arrValoresParam.Add(obj.feccre == null ? null : "'" + Convert.ToDateTime(obj.feccre).ToString(CParametros.ParFormatoFechaHora) + "'");
				arrValoresParam.Add(obj.usumod == null ? null : "'" + obj.usumod + "'");
				arrValoresParam.Add(obj.fecmod == null ? null : "'" + Convert.ToDateTime(obj.fecmod).ToString(CParametros.ParFormatoFechaHora) + "'");

				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				return local.execStoreProcedure(strNombreSp, arrNombreParam, arrValoresParam);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta un nuevo registro en la tabla postulante a partir de una clase del tipo Epostulante
		/// </summary>
		/// <param name="strNombreSp" type="System.string">
		///     <para>
		/// 		 Nombre del Procedimiento a ejecutar sobre el SP
		///     </para>
		/// </param>
		/// <param name="obj" type="Entidades.EntPosTulante">
		///     <para>
		/// 		 Clase desde la que se va a ejecutar el SP de la tabla postulante
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor de registros afectados en el Procedimiento de la tabla postulante
		/// </returns>
		public int EjecutarSpDesdeObjeto(string strNombreSp, EntPosTulante obj, ref CTrans localTrans)
		{
			try
			{
				
				ArrayList arrNombreParam = new ArrayList();
				arrNombreParam.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.apiestado.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.usucre.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.feccre.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.usumod.ToString());
				arrNombreParam.Add(EntPosTulante.Fields.fecmod.ToString());
				
				ArrayList arrValoresParam = new ArrayList();
				arrValoresParam.Add(obj.id_postulante);
				arrValoresParam.Add(obj.ci_postulante);
				arrValoresParam.Add(obj.nombre_postulante == null ? null : "'" + obj.nombre_postulante + "'");
				arrValoresParam.Add(obj.ap_mat_postulante == null ? null : "'" + obj.ap_mat_postulante + "'");
				arrValoresParam.Add(obj.ap_pat_postulante == null ? null : "'" + obj.ap_pat_postulante + "'");
				arrValoresParam.Add(obj.celular_postulante);
				arrValoresParam.Add(obj.fecha_nac_postulante == null ? null : "'" + Convert.ToDateTime(obj.fecha_nac_postulante).ToString(CParametros.ParFormatoFechaHora) + "'");
				arrValoresParam.Add(obj.apiestado == null ? null : "'" + obj.apiestado + "'");
				arrValoresParam.Add(obj.usucre == null ? null : "'" + obj.usucre + "'");
				arrValoresParam.Add(obj.feccre == null ? null : "'" + Convert.ToDateTime(obj.feccre).ToString(CParametros.ParFormatoFechaHora) + "'");
				arrValoresParam.Add(obj.usumod == null ? null : "'" + obj.usumod + "'");
				arrValoresParam.Add(obj.fecmod == null ? null : "'" + Convert.ToDateTime(obj.fecmod).ToString(CParametros.ParFormatoFechaHora) + "'");

				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				return local.execStoreProcedure(strNombreSp, arrNombreParam, arrValoresParam, ref localTrans);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}


		#endregion 

		#region FuncionesAgregadas

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] COUNT
		/// </summary>
		/// <param name="refField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesCount(EntPosTulante.Fields refField)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(1);
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(1);
				
				return FuncionesCount(refField, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] COUNT
		/// </summary>
		/// <param name="refField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="whereField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Columna que va a filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="valueField" type="System.Object">
		///     <para>
		/// 		 Valor para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesCount(EntPosTulante.Fields refField, EntPosTulante.Fields whereField, object valueField)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(whereField.ToString());
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(valueField.ToString());
				
				return FuncionesCount(refField, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] COUNT
		/// </summary>
		/// <param name="refField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesCount(EntPosTulante.Fields refField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add("count(" + refField + ")");
				DataTable dtTemp = ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere);
				if (dtTemp.Rows.Count == 0)
					throw new Exception("La consulta no ha devuelto resultados.");
				if (dtTemp.Rows.Count > 1)
					throw new Exception("Se ha devuelto mas de una fila.");
				if (dtTemp.Rows[0][0] == null)
					return 0;
				if (dtTemp.Rows[0][0] == "")
					return 0;
				return int.Parse(dtTemp.Rows[0][0].ToString());
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] MIN
		/// </summary>
		/// <param name="refField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesMin(EntPosTulante.Fields refField)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(1);
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(1);
				
				return FuncionesMin(refField, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] MIN
		/// </summary>
		/// <param name="refField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="whereField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Columna que va a filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="valueField" type="System.Object">
		///     <para>
		/// 		 Valor para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesMin(EntPosTulante.Fields refField, EntPosTulante.Fields whereField, object valueField)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(whereField.ToString());
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(valueField.ToString());
				
				return FuncionesMin(refField, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] MIN
		/// </summary>
		/// <param name="refField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesMin(EntPosTulante.Fields refField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add("min(" + refField + ")");
				DataTable dtTemp = ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere);
				if (dtTemp.Rows.Count == 0)
					throw new Exception("La consulta no ha devuelto resultados.");
				if (dtTemp.Rows.Count > 1)
					throw new Exception("Se ha devuelto mas de una fila.");
				if (dtTemp.Rows[0][0] == null)
					return 0;
				if (dtTemp.Rows[0][0] == "")
					return 0;
				return int.Parse(dtTemp.Rows[0][0].ToString());
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] MAX
		/// </summary>
		/// <param name="refField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesMax(EntPosTulante.Fields refField)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(1);
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(1);
				
				return FuncionesMax(refField, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] MAX
		/// </summary>
		/// <param name="refField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="whereField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Columna que va a filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="valueField" type="System.Object">
		///     <para>
		/// 		 Valor para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesMax(EntPosTulante.Fields refField, EntPosTulante.Fields whereField, object valueField)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(whereField.ToString());
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(valueField.ToString());
				
				return FuncionesMax(refField, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] MAX
		/// </summary>
		/// <param name="refField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesMax(EntPosTulante.Fields refField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add("max(" + refField + ")");
				DataTable dtTemp = ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere);
				if (dtTemp.Rows.Count == 0)
					throw new Exception("La consulta no ha devuelto resultados.");
				if (dtTemp.Rows.Count > 1)
					throw new Exception("Se ha devuelto mas de una fila.");
				if (dtTemp.Rows[0][0] == null)
					return 0;
				if (dtTemp.Rows[0][0] == "")
					return 0;
				return int.Parse(dtTemp.Rows[0][0].ToString());
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] SUM
		/// </summary>
		/// <param name="refField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesSum(EntPosTulante.Fields refField)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(1);
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(1);
				
				return FuncionesSum(refField, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] SUM
		/// </summary>
		/// <param name="refField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="whereField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Columna que va a filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="valueField" type="System.Object">
		///     <para>
		/// 		 Valor para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesSum(EntPosTulante.Fields refField, EntPosTulante.Fields whereField, object valueField)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(whereField.ToString());
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(valueField.ToString());
				
				return FuncionesSum(refField, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] SUM
		/// </summary>
		/// <param name="refField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesSum(EntPosTulante.Fields refField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add("sum(" + refField + ")");
				DataTable dtTemp = ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere);
				if (dtTemp.Rows.Count == 0)
					throw new Exception("La consulta no ha devuelto resultados.");
				if (dtTemp.Rows.Count > 1)
					throw new Exception("Se ha devuelto mas de una fila.");
				if (dtTemp.Rows[0][0] == null)
					return 0;
				if (dtTemp.Rows[0][0] == "")
					return 0;
				return int.Parse(dtTemp.Rows[0][0].ToString());
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] AVG
		/// </summary>
		/// <param name="refField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesAvg(EntPosTulante.Fields refField)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(1);
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(1);
				
				return FuncionesAvg(refField, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] AVG
		/// </summary>
		/// <param name="refField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="whereField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Columna que va a filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="valueField" type="System.Object">
		///     <para>
		/// 		 Valor para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesAvg(EntPosTulante.Fields refField, EntPosTulante.Fields whereField, object valueField)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(whereField.ToString());
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(valueField.ToString());
				
				return FuncionesAvg(refField, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] AVG
		/// </summary>
		/// <param name="refField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntPosTulante que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesAvg(EntPosTulante.Fields refField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add("avg(" + refField + ")");
				DataTable dtTemp = ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere);
				if (dtTemp.Rows.Count == 0)
					throw new Exception("La consulta no ha devuelto resultados.");
				if (dtTemp.Rows.Count > 1)
					throw new Exception("Se ha devuelto mas de una fila.");
				if (dtTemp.Rows[0][0] == null)
					return 0;
				if (dtTemp.Rows[0][0] == "")
					return 0;
				return int.Parse(dtTemp.Rows[0][0].ToString());
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}


		#endregion 

		#region ABMs

		/// <summary>
		/// 	Funcion que inserta un nuevo registro en la tabla postulante a partir de una clase del tipo Epostulante
		/// </summary>
		/// <param name="obj" type="Entidades.EntPosTulante">
		///     <para>
		/// 		 Clase desde la que se van a insertar los valores a la tabla postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor TRUE or FALSE que indica el exito de la operacionpostulante
		/// </returns>
		public bool Insert(EntPosTulante obj, bool bValidar = true)
		{
			try
			{
				ArrayList arrNombreParam = new ArrayList();
				ArrayList arrValoresParam = new ArrayList();
				arrNombreParam.Add("id_postulante");
				arrValoresParam.Add(null);
				arrNombreParam.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrValoresParam.Add(obj.ci_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrValoresParam.Add(obj.nombre_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrValoresParam.Add(obj.ap_mat_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrValoresParam.Add(obj.ap_pat_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrValoresParam.Add(obj.celular_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrValoresParam.Add(obj.fecha_nac_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.usucre.ToString());
				arrValoresParam.Add(obj.usucre);
				
				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				string nombreSp = CListadoSP.PosTulante.sp_postulante_Ins.ToString();
				return (local.execStoreProcedure(nombreSp, arrNombreParam, arrValoresParam) > 0);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta un nuevo registro en la tabla postulante a partir de una clase del tipo Epostulante
		/// </summary>
		/// <param name="obj" type="Entidades.EntPosTulante">
		///     <para>
		/// 		 Clase desde la que se van a insertar los valores a la tabla postulante
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor TRUE or FALSE que indica el exito de la operacionpostulante
		/// </returns>
		public bool Insert(EntPosTulante obj, ref CTrans localTrans, bool bValidar = true)
		{
			try
			{
				ArrayList arrNombreParam = new ArrayList();
				ArrayList arrValoresParam = new ArrayList();
				arrNombreParam.Add("id_postulante");
				arrValoresParam.Add("");
				arrNombreParam.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrValoresParam.Add(obj.ci_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrValoresParam.Add(obj.nombre_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrValoresParam.Add(obj.ap_mat_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrValoresParam.Add(obj.ap_pat_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrValoresParam.Add(obj.celular_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrValoresParam.Add(obj.fecha_nac_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.usucre.ToString());
				arrValoresParam.Add(obj.usucre);
				
				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				string nombreSp = CListadoSP.PosTulante.sp_postulante_Ins.ToString();
				return (local.execStoreProcedure(nombreSp, arrNombreParam, arrValoresParam, ref localTrans) > 0);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta un nuevo registro en la tabla postulante a partir de una clase del tipo Epostulante
		/// </summary>
		/// <param name="obj" type="Entidades.EntPosTulante">
		///     <para>
		/// 		 Clase desde la que se van a insertar los valores a la tabla postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor que indica la cantidad de registros actualizados en postulante
		/// </returns>
		public int Update(EntPosTulante obj, bool bValidar = true)
		{
			try
			{
				ArrayList arrNombreParam = new ArrayList();
				ArrayList arrValoresParam = new ArrayList();
				arrNombreParam.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrValoresParam.Add(obj.id_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrValoresParam.Add(obj.ci_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrValoresParam.Add(obj.nombre_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrValoresParam.Add(obj.ap_mat_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrValoresParam.Add(obj.ap_pat_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrValoresParam.Add(obj.celular_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrValoresParam.Add(obj.fecha_nac_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.usumod.ToString());
				arrValoresParam.Add(obj.usumod);
				
				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				string nombreSp = CListadoSP.PosTulante.sp_postulante_Upd.ToString();
				return local.execStoreProcedure(nombreSp, arrNombreParam, arrValoresParam);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta un nuevo registro en la tabla postulante a partir de una clase del tipo Epostulante
		/// </summary>
		/// <param name="obj" type="Entidades.EntPosTulante">
		///     <para>
		/// 		 Clase desde la que se van a insertar los valores a la tabla postulante
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor TRUE or FALSE que indica el exito de la operacionpostulante
		/// </returns>
		public int Update(EntPosTulante obj, ref CTrans localTrans, bool bValidar = true)
		{
			try
			{
				ArrayList arrNombreParam = new ArrayList();
				ArrayList arrValoresParam = new ArrayList();
				arrNombreParam.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrValoresParam.Add(obj.id_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrValoresParam.Add(obj.ci_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrValoresParam.Add(obj.nombre_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrValoresParam.Add(obj.ap_mat_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrValoresParam.Add(obj.ap_pat_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrValoresParam.Add(obj.celular_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrValoresParam.Add(obj.fecha_nac_postulante);
				
				arrNombreParam.Add(EntPosTulante.Fields.usumod.ToString());
				arrValoresParam.Add(obj.usumod);
				
				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				string nombreSp = CListadoSP.PosTulante.sp_postulante_Upd.ToString();
				return local.execStoreProcedure(nombreSp, arrNombreParam, arrValoresParam, ref localTrans);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta un nuevo registro en la tabla postulante a partir de una clase del tipo Epostulante
		/// </summary>
		/// <param name="obj" type="Entidades.EntPosTulante">
		///     <para>
		/// 		 Clase desde la que se van a insertar los valores a la tabla postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor TRUE or FALSE que indica el exito de la operacionpostulante
		/// </returns>
		public int Delete(EntPosTulante obj, bool bValidar = true)
		{
			try
			{
				ArrayList arrNombreParam = new ArrayList();
				ArrayList arrValoresParam = new ArrayList();
				arrNombreParam.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrValoresParam.Add(obj.id_postulante);
				
				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				string nombreSp = CListadoSP.PosTulante.sp_postulante_Del.ToString();
				return local.execStoreProcedure(nombreSp, arrNombreParam, arrValoresParam);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta un nuevo registro en la tabla postulante a partir de una clase del tipo Epostulante
		/// </summary>
		/// <param name="obj" type="Entidades.EntPosTulante">
		///     <para>
		/// 		 Clase desde la que se van a insertar los valores a la tabla postulante
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor TRUE or FALSE que indica el exito de la operacionpostulante
		/// </returns>
		public int Delete(EntPosTulante obj, ref CTrans localTrans, bool bValidar = true)
		{
			try
			{
				ArrayList arrNombreParam = new ArrayList();
				ArrayList arrValoresParam = new ArrayList();
				arrNombreParam.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrValoresParam.Add(obj.id_postulante);
				
				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				string nombreSp = CListadoSP.PosTulante.sp_postulante_Del.ToString();
				return local.execStoreProcedure(nombreSp, arrNombreParam, arrValoresParam, ref localTrans);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta o actualiza un registro un nuevo registro en la tabla postulante a partir de una clase del tipo Epostulante
		/// </summary>
		/// <param name="obj" type="Entidades.EntPosTulante">
		///     <para>
		/// 		 Clase desde la que se van a insertar o actualizar los valores a la tabla postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor TRUE or FALSE que indica el exito de la operacionpostulante
		/// </returns>
		public int InsertUpdate(EntPosTulante obj)
		{
			try
			{
				bool esInsertar = true;
				
					esInsertar = (esInsertar && (obj.id_postulante == null));
				
				if (esInsertar)
					return Insert(obj) ? 1 : 0;
				else
					return Update(obj);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta o actualiza un registro un nuevo registro en la tabla postulante a partir de una clase del tipo Epostulante
		/// </summary>
		/// <param name="obj" type="Entidades.EntPosTulante">
		///     <para>
		/// 		 Clase desde la que se van a insertar o actualizar los valores a la tabla postulante
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion postulante
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor TRUE or FALSE que indica el exito de la operacionpostulante
		/// </returns>
		public int InsertUpdate(EntPosTulante obj, ref CTrans localTrans)
		{
			try
			{
				bool esInsertar = false;
				
					esInsertar = (esInsertar && (obj.id_postulante == null));
				
				if (esInsertar)
					return Insert(obj, ref localTrans) ? 1 : 0;
				else
					return Update(obj, ref localTrans);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}


		#endregion 

		#region Llenado de elementos

		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla postulante
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo DropDownList en el que se van a cargar los datos de la tabla postulante
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(1);
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(1);
				
				CargarDropDownList(ref cmb, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla postulante
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo DropDownList en el que se van a cargar los datos de la tabla postulante
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(1);
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(1);
				
				CargarDropDownList(ref cmb, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla postulante
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo DropDownList en el que se van a cargar los datos de la tabla postulante
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				CargarDropDownList(ref cmb, arrColumnasWhere, arrValoresWhere, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla postulante
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo DropDownList en el que se van a cargar los datos de la tabla postulante
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				
				CargarDropDownList(ref cmb, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla postulante
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla postulante
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntPosTulante.Fields valueField, EntPosTulante.Fields textField)
		{
			try
			{
				CargarDropDownList(ref cmb, valueField, textField, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla postulante
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla postulante
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntPosTulante.Fields valueField, EntPosTulante.Fields textField, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(valueField.ToString());
				arrColumnas.Add(textField.ToString());
				
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(1);
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(1);
				
				CargarDropDownList(ref cmb, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla postulante
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla postulante
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="String">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntPosTulante.Fields valueField, String textField)
		{
			try
			{
				CargarDropDownList(ref cmb, valueField, textField, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla postulante
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla postulante
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="String">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntPosTulante.Fields valueField, String textField, String strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(valueField.ToString());
				arrColumnas.Add(textField);
				
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(1);
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(1);
				
				CargarDropDownList(ref cmb, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla postulante
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla postulante
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntPosTulante.Fields valueField, EntPosTulante.Fields textField, EntPosTulante.Fields searchField, object searchValue)
		{
			try
			{
				CargarDropDownList(ref cmb, valueField, textField.ToString(), searchField, searchValue);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla postulante
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla postulante
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="String">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntPosTulante.Fields valueField, String textField, EntPosTulante.Fields searchField, object searchValue)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(valueField.ToString());
				arrColumnas.Add(textField);
				
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				
				CargarDropDownList(ref cmb, arrColumnas, arrColumnasWhere, arrValoresWhere, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla postulante
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla postulante
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntPosTulante.Fields valueField, EntPosTulante.Fields textField, EntPosTulante.Fields searchField, object searchValue, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(valueField.ToString());
				arrColumnas.Add(textField.ToString());
				
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				
				CargarDropDownList(ref cmb, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla postulante
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla postulante
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchField" type="String">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntPosTulante.Fields valueField, String textField, EntPosTulante.Fields searchField, object searchValue, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(valueField.ToString());
				arrColumnas.Add(textField);
				
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				
				CargarDropDownList(ref cmb, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla postulante
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla postulante
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntPosTulante.Fields valueField, EntPosTulante.Fields textField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(valueField.ToString());
				arrColumnas.Add(textField.ToString());
				
				CargarDropDownList(ref cmb, arrColumnas, arrColumnasWhere, arrValoresWhere, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla postulante
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla postulante
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="String">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntPosTulante.Fields valueField, String textField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(valueField.ToString());
				arrColumnas.Add(textField);
				
				CargarDropDownList(ref cmb, arrColumnas, arrColumnasWhere, arrValoresWhere, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla postulante
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla postulante
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntPosTulante.Fields valueField, EntPosTulante.Fields textField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(valueField.ToString());
				arrColumnas.Add(textField.ToString());
				
				CargarDropDownList(ref cmb, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla postulante
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla postulante
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="String">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntPosTulante.Fields valueField, String textField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(valueField.ToString());
				arrColumnas.Add(textField);
				
				CargarDropDownList(ref cmb, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla postulante
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla postulante
		///     </para>
		/// </param>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntPosTulante.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Columns.Count > 0)
				{
					cmb.DataValueField = table.Columns[0].ColumnName;
					cmb.DataTextField = table.Columns[1].ColumnName;
					cmb.DataSource = table;
					cmb.DataBind();

				}
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla postulante
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView 
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
			
				
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add("'1'");
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add("'1'");
				
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla postulante
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView 
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		  Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, String strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
			
				
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add("'1'");
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add("'1'");
				
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla postulante
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView 
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, ArrayList arrColumnas)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add("'1'");
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add("'1'");
				
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla postulante
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView 
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="string">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, ArrayList arrColumnas, String strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add("'1'");
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add("'1'");
				
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla postulante
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView 
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
			
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla postulante
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView 
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="string">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, String strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
			
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla postulante
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView 
		///     </para>
		/// </param>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla postulante
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView 
		///     </para>
		/// </param>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="string">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				CConn local = new CConn();
				DbDataReader dsReader = local.cargarDataReaderAnd(CParametros.schema + EntPosTulante.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				dtg.DataSource = dsReader;
				dtg.DataBind();

			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla postulante
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, EntPosTulante.Fields searchField, object searchValue)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
				
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla postulante
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="string">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, EntPosTulante.Fields searchField, object searchValue, String strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntPosTulante.Fields.id_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ci_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.nombre_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_mat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.ap_pat_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.celular_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecha_nac_postulante.ToString());
				arrColumnas.Add(EntPosTulante.Fields.apiestado.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usucre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.feccre.ToString());
				arrColumnas.Add(EntPosTulante.Fields.usumod.ToString());
				arrColumnas.Add(EntPosTulante.Fields.fecmod.ToString());
				
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla postulante
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView
		///     </para>
		/// </param>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, ArrayList arrColumnas, EntPosTulante.Fields searchField, object searchValue)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla postulante
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView
		///     </para>
		/// </param>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntPosTulante.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="string">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, ArrayList arrColumnas, EntPosTulante.Fields searchField, object searchValue, String strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla postulante
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView 
		///     </para>
		/// </param>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarGridViewOr(ref GridView dtg, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				CargarGridViewOr(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla postulante
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView 
		///     </para>
		/// </param>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParametrosAdicionales" type="string">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarGridViewOr(ref GridView dtg, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParametrosAdicionales)
		{
			try
			{
				CConn local = new CConn();
				DbDataReader dsReader = local.cargarDataReaderOr(CParametros.schema + EntPosTulante.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
				
				dtg.DataSource = dsReader;
				dtg.DataBind();

			}
			catch (Exception exp)
			{
				throw exp;
			}
		}


		#endregion 

		#region Funciones Internas

		/// <summary>
		/// 	 Funcion que devuelve un objeto a partir de un DataRow
		/// </summary>
		/// <param name="row" type="System.Data.DataRow">
		///     <para>
		/// 		 DataRow con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Objeto postulante
		/// </returns>
		internal EntPosTulante crearObjeto(DataRow row)
		{
			EntPosTulante obj = new EntPosTulante();
			obj.id_postulante = (int) GetColumnType(row[EntPosTulante.Fields.id_postulante.ToString()], EntPosTulante.Fields.id_postulante);
			obj.ci_postulante = (int?) GetColumnType(row[EntPosTulante.Fields.ci_postulante.ToString()], EntPosTulante.Fields.ci_postulante);
			obj.nombre_postulante = (string) GetColumnType(row[EntPosTulante.Fields.nombre_postulante.ToString()], EntPosTulante.Fields.nombre_postulante);
			obj.ap_mat_postulante = (string) GetColumnType(row[EntPosTulante.Fields.ap_mat_postulante.ToString()], EntPosTulante.Fields.ap_mat_postulante);
			obj.ap_pat_postulante = (string) GetColumnType(row[EntPosTulante.Fields.ap_pat_postulante.ToString()], EntPosTulante.Fields.ap_pat_postulante);
			obj.celular_postulante = (int?) GetColumnType(row[EntPosTulante.Fields.celular_postulante.ToString()], EntPosTulante.Fields.celular_postulante);
			obj.fecha_nac_postulante = (DateTime?) GetColumnType(row[EntPosTulante.Fields.fecha_nac_postulante.ToString()], EntPosTulante.Fields.fecha_nac_postulante);
			obj.apiestado = (string) GetColumnType(row[EntPosTulante.Fields.apiestado.ToString()], EntPosTulante.Fields.apiestado);
			obj.usucre = (string) GetColumnType(row[EntPosTulante.Fields.usucre.ToString()], EntPosTulante.Fields.usucre);
			obj.feccre = (DateTime) GetColumnType(row[EntPosTulante.Fields.feccre.ToString()], EntPosTulante.Fields.feccre);
			obj.usumod = (string) GetColumnType(row[EntPosTulante.Fields.usumod.ToString()], EntPosTulante.Fields.usumod);
			obj.fecmod = (DateTime?) GetColumnType(row[EntPosTulante.Fields.fecmod.ToString()], EntPosTulante.Fields.fecmod);
			return obj;
		}

		/// <summary>
		/// 	 Funcion que devuelve un objeto a partir de un DataRow
		/// </summary>
		/// <param name="row" type="System.Data.DataRow">
		///     <para>
		/// 		 DataRow con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Objeto postulante
		/// </returns>
		internal EntPosTulante crearObjetoRevisado(DataRow row)
		{
			EntPosTulante obj = new EntPosTulante();
			if (row.Table.Columns.Contains(EntPosTulante.Fields.id_postulante.ToString()))
				obj.id_postulante = (int) GetColumnType(row[EntPosTulante.Fields.id_postulante.ToString()], EntPosTulante.Fields.id_postulante);
			if (row.Table.Columns.Contains(EntPosTulante.Fields.ci_postulante.ToString()))
				obj.ci_postulante = (int?) GetColumnType(row[EntPosTulante.Fields.ci_postulante.ToString()], EntPosTulante.Fields.ci_postulante);
			if (row.Table.Columns.Contains(EntPosTulante.Fields.nombre_postulante.ToString()))
				obj.nombre_postulante = (string) GetColumnType(row[EntPosTulante.Fields.nombre_postulante.ToString()], EntPosTulante.Fields.nombre_postulante);
			if (row.Table.Columns.Contains(EntPosTulante.Fields.ap_mat_postulante.ToString()))
				obj.ap_mat_postulante = (string) GetColumnType(row[EntPosTulante.Fields.ap_mat_postulante.ToString()], EntPosTulante.Fields.ap_mat_postulante);
			if (row.Table.Columns.Contains(EntPosTulante.Fields.ap_pat_postulante.ToString()))
				obj.ap_pat_postulante = (string) GetColumnType(row[EntPosTulante.Fields.ap_pat_postulante.ToString()], EntPosTulante.Fields.ap_pat_postulante);
			if (row.Table.Columns.Contains(EntPosTulante.Fields.celular_postulante.ToString()))
				obj.celular_postulante = (int?) GetColumnType(row[EntPosTulante.Fields.celular_postulante.ToString()], EntPosTulante.Fields.celular_postulante);
			if (row.Table.Columns.Contains(EntPosTulante.Fields.fecha_nac_postulante.ToString()))
				obj.fecha_nac_postulante = (DateTime?) GetColumnType(row[EntPosTulante.Fields.fecha_nac_postulante.ToString()], EntPosTulante.Fields.fecha_nac_postulante);
			if (row.Table.Columns.Contains(EntPosTulante.Fields.apiestado.ToString()))
				obj.apiestado = (string) GetColumnType(row[EntPosTulante.Fields.apiestado.ToString()], EntPosTulante.Fields.apiestado);
			if (row.Table.Columns.Contains(EntPosTulante.Fields.usucre.ToString()))
				obj.usucre = (string) GetColumnType(row[EntPosTulante.Fields.usucre.ToString()], EntPosTulante.Fields.usucre);
			if (row.Table.Columns.Contains(EntPosTulante.Fields.feccre.ToString()))
				obj.feccre = (DateTime) GetColumnType(row[EntPosTulante.Fields.feccre.ToString()], EntPosTulante.Fields.feccre);
			if (row.Table.Columns.Contains(EntPosTulante.Fields.usumod.ToString()))
				obj.usumod = (string) GetColumnType(row[EntPosTulante.Fields.usumod.ToString()], EntPosTulante.Fields.usumod);
			if (row.Table.Columns.Contains(EntPosTulante.Fields.fecmod.ToString()))
				obj.fecmod = (DateTime?) GetColumnType(row[EntPosTulante.Fields.fecmod.ToString()], EntPosTulante.Fields.fecmod);
			return obj;
		}

		/// <summary>
		/// 	 Funcion que crea una Lista de objetos a partir de un DataTable
		/// </summary>
		/// <param name="dtpostulante" type="System.Data.DateTable">
		///     <para>
		/// 		 DataTable con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Lista de Objetos postulante
		/// </returns>
		internal List<EntPosTulante> crearLista(DataTable dtpostulante)
		{
			List<EntPosTulante> list = new List<EntPosTulante>();
			
			EntPosTulante obj = new EntPosTulante();
			foreach (DataRow row in dtpostulante.Rows)
			{
				obj = crearObjeto(row);
				list.Add(obj);
			}
			return list;
		}
		
		/// <summary>
		/// 	 Funcion que crea una Lista de objetos a partir de un DataTable y con solo algunas columnas
		/// </summary>
		/// <param name="dtpostulante" type="System.Data.DateTable">
		///     <para>
		/// 		 DataTable con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Lista de Objetos postulante
		/// </returns>
		internal List<EntPosTulante> crearListaRevisada(DataTable dtpostulante)
		{
			List<EntPosTulante> list = new List<EntPosTulante>();
			
			EntPosTulante obj = new EntPosTulante();
			foreach (DataRow row in dtpostulante.Rows)
			{
				obj = crearObjetoRevisado(row);
				list.Add(obj);
			}
			return list;
		}
		
		/// <summary>
		/// 	 Funcion que crea una Lista de objetos a partir de un DataTable
		/// </summary>
		/// <param name="dtpostulante" type="System.Data.DateTable">
		///     <para>
		/// 		 DataTable con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Cola de Objetos postulante
		/// </returns>
		internal Queue<EntPosTulante> crearCola(DataTable dtpostulante)
		{
			Queue<EntPosTulante> cola = new Queue<EntPosTulante>();
			
			EntPosTulante obj = new EntPosTulante();
			foreach (DataRow row in dtpostulante.Rows)
			{
				obj = crearObjeto(row);
				cola.Enqueue(obj);
			}
			return cola;
		}
		
		/// <summary>
		/// 	 Funcion que crea una Lista de objetos a partir de un DataTable
		/// </summary>
		/// <param name="dtpostulante" type="System.Data.DateTable">
		///     <para>
		/// 		 DataTable con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Pila de Objetos postulante
		/// </returns>
		internal Stack<EntPosTulante> crearPila(DataTable dtpostulante)
		{
			Stack<EntPosTulante> pila = new Stack<EntPosTulante>();
			
			EntPosTulante obj = new EntPosTulante();
			foreach (DataRow row in dtpostulante.Rows)
			{
				obj = crearObjeto(row);
				pila.Push(obj);
			}
			return pila;
		}
		
		/// <summary>
		/// 	 Funcion que crea un Dicionario a partir de un DataTable
		/// </summary>
		/// <param name="dtpostulante" type="System.Data.DateTable">
		///     <para>
		/// 		 DataTable con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Diccionario de Objetos postulante
		/// </returns>
		internal Dictionary<String, EntPosTulante> crearDiccionario(DataTable dtpostulante)
		{
			Dictionary<String, EntPosTulante>  miDic = new Dictionary<String, EntPosTulante>();
			
			EntPosTulante obj = new EntPosTulante();
			foreach (DataRow row in dtpostulante.Rows)
			{
				obj = crearObjeto(row);
				miDic.Add(obj.id_postulante.ToString(), obj);
			}
			return miDic;
		}
		
		/// <summary>
		/// 	 Funcion que crea un Dicionario a partir de un DataTable
		/// </summary>
		/// <param name="dtpostulante" type="System.Data.DateTable">
		///     <para>
		/// 		 DataTable con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 HashTable de Objetos postulante
		/// </returns>
		internal Hashtable crearHashTable(DataTable dtpostulante)
		{
			Hashtable miTabla = new Hashtable();
			
			EntPosTulante obj = new EntPosTulante();
			foreach (DataRow row in dtpostulante.Rows)
			{
				obj = crearObjeto(row);
				miTabla.Add(obj.id_postulante.ToString(), obj);
			}
			return miTabla;
		}
		
		/// <summary>
		/// 	 Funcion que crea un Dicionario a partir de un DataTable y solo con columnas existentes
		/// </summary>
		/// <param name="dtpostulante" type="System.Data.DateTable">
		///     <para>
		/// 		 DataTable con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Diccionario de Objetos postulante
		/// </returns>
		internal Dictionary<String, EntPosTulante> crearDiccionarioRevisado(DataTable dtpostulante)
		{
			Dictionary<String, EntPosTulante>  miDic = new Dictionary<String, EntPosTulante>();
			
			EntPosTulante obj = new EntPosTulante();
			foreach (DataRow row in dtpostulante.Rows)
			{
				obj = crearObjetoRevisado(row);
				miDic.Add(obj.id_postulante.ToString(), obj);
			}
			return miDic;
		}
		
		internal Dictionary<String, EntPosTulante> crearDiccionario(DataTable dtpostulante, EntPosTulante.Fields dicKey)
		{
			Dictionary<String, EntPosTulante>  miDic = new Dictionary<String, EntPosTulante>();
			
			EntPosTulante obj = new EntPosTulante();
			foreach (DataRow row in dtpostulante.Rows)
			{
				obj = crearObjeto(row);
				
				var nameOfProperty = dicKey.ToString();
				var propertyInfo = obj.GetType().GetProperty(nameOfProperty);
				var value = propertyInfo.GetValue(obj, null);
				
				miDic.Add(value.ToString(), obj);
			}
			return miDic;
		}
		
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
		
		protected void Finalize()
		{
			Dispose();
		}
		#endregion

	}
}

