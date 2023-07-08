using System;
using System.Data;

namespace DataAccess
{
	public static class DB
	{
		public static T Write<T>(DataTable dt, int iRow, int iColumn)
		{
			if (dt == null)
			{
				return default(T);
			}
			else
			{
				if (iColumn <= dt.Columns.Count)
				{
					return Write<T>(dt, dt.Columns[iColumn].ColumnName, iRow);
				}
				else
				{
					if (typeof(T) == typeof(string))
					{
						return (T)Convert.ChangeType("", typeof(T));
					}
					else
					{
						return default(T);
					}
				}
			}
		}


		public static T Write<T>(DataTable dt, int iColumn) => Write<T>(dt, 0, iColumn);

		public static T Write<T>(DataTable dt, string strColumn, int iRow = 0)
		{
			if (dt == null)
			{
				if (typeof(T) == typeof(string))
				{
					return (T)Convert.ChangeType("", typeof(T));
				}
				else
				{
					return default(T);
				}
			}
			else
			{
				if (dt.Rows.Count > 0)
				{
					if (dt.Rows[iRow][strColumn] != null)
					{
						if (dt.Rows[iRow][strColumn] is DBNull)
						{
							if (typeof(T) == typeof(string))
							{
								return (T)Convert.ChangeType("", typeof(T));
							}
							else
							{
								return default(T);
							}
						}
						else
						{
							if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>))
							{
								return (T)Convert.ChangeType(dt.Rows[iRow][strColumn], Nullable.GetUnderlyingType(typeof(T)));
							}
							else if (typeof(T).IsEnum)
							{
								return (T)dt.Rows[iRow][strColumn];
							}
							else
							{
								return (T)Convert.ChangeType(dt.Rows[iRow][strColumn], typeof(T));
							}
						}
					}
					else
					{
						if (typeof(T) == typeof(string))
						{
							return (T)Convert.ChangeType("", typeof(T));
						}
						else
						{
							return default(T);
						}
					}
				}
				else
				{
					if (typeof(T) == typeof(string))
					{
						return (T)Convert.ChangeType("", typeof(T));
					}
					else
					{
						return default(T);
					}
				}

			}
		}

		public static T Write<T>(DataSet ds, int iColumn, int iTable = 0) => Write<T>(ds.Tables[iTable], iColumn);

		public static T Write<T>(DataSet ds, string strColumn, string table)
		{
			if (ds != null && ds.Tables.Contains(table))
			{
				return Write<T>(ds.Tables[table], strColumn);
			}

			return default(T);
		}


		public static T Write<T>(DataSet ds, string strColumn, int iTable = 0, int iRow = 0)
		{
			if (ds != null && ds.Tables.Count > iTable)
			{
				return Write<T>(ds.Tables[iTable], strColumn, iRow);
			}

			return default(T);
		}

		public static int Rows(DataTable dt) => dt == null ? 0 : dt.Rows.Count;

		public static int Rows(DataSet ds, string tableName) => ds == null ? 0 : ds.Tables[tableName].Rows.Count;

		public static int RowCount(this DataSet dataSet, int tableIndex) => Rows(dataSet, tableIndex);

		public static int RowCount(this DataSet dataSet, string tableName = "") => Rows(dataSet, string.IsNullOrEmpty(tableName) ? 0 : dataSet.Tables.IndexOf(tableName));

		public static int Rows(DataSet ds, int iTable = 0) => ds == null ? 0 : ds.Tables[iTable].Rows.Count;

		public static DataTable TopNRows(DataTable dtImport, int iRows, int iStart = 0)
		{
			var dtTop = dtImport.Clone();
			var importRows = dtImport.Select();

			for (int iLoop = iStart; iLoop < iRows + iStart; iLoop++)
			{
				if (iLoop < importRows.Length)
				{
					dtTop.ImportRow(importRows[iLoop]);
					dtTop.AcceptChanges();
				}
			}

			return dtTop;
		}

		public static string CheckParameterLength(object objTest, int iMaxChars, bool escapeHtmlMarkup = true)
		{
			string strTest;

			if (objTest == null)
			{
				strTest = "";
			}

			else
			{
				strTest = objTest.ToString();
			}

			if (strTest.Length > iMaxChars)
			{
				strTest = Left(strTest.Trim(), iMaxChars);
			}

			if (escapeHtmlMarkup)
			{
				strTest = strTest.Replace("<", "&lt;");
				strTest = strTest.Replace(">", "&gt;");
			}

			return strTest;
		}

		public static string Left(string s, int len)
		{
			if (len == 0 || s.Length == 0)
			{
				return "";
			}
			else if (s.Length <= len)
			{
				return s;
			}
			else
			{
				return s.Substring(0, len);
			}

		}


		#region "Extension methods"

		public static int RowCount(this DataTable table) => Rows(table);

		public static T Field<T>(this DataSet data, string column) => Write<T>(data, column);

		public static T Field<T>(this DataSet data, int table, string column) => Write<T>(data, column, table);

		public static T Field<T>(this DataSet data, string table, string column) => Write<T>(data, column, table);

		public static T Field<T>(this DataTable table, string column) => Write<T>(table, column);


		#endregion

	}
}
