using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;

namespace DataAccess
{
	public static class DataTableExtensions
	{
		public static List<dynamic> ToDynamic(this DataTable dt)
		{
			return (from DataRow row in dt.Rows select row.ToDynamic()).ToList();
		}

		public static dynamic ToDynamic(this DataRow row)
		{
			dynamic dyn = new ExpandoObject();
			foreach (DataColumn column in row.Table.Columns)
			{
				var dic = (IDictionary<string, object>)dyn;
				dic[column.ColumnName] = row[column];
			}
			return dyn;
		}

		public static dynamic ToDynamic(this DataRow row, string[] ignoredColumns)
		{
			dynamic dyn = new ExpandoObject();
			foreach (DataColumn column in row.Table.Columns)
			{
				if (!ignoredColumns.Contains(column.ColumnName))
				{
					var dic = (IDictionary<string, object>)dyn;
					dic[column.ColumnName] = row[column];
				}
			}
			return dyn;
		}

		public static List<dynamic> ToDynamic(this DataRow[] rows) => (from DataRow row in rows select row.ToDynamic()).ToList();

		public static DataTable SelectToDataTable(this DataTable table, string select)
		{
			var rows = table.Select(select);
			return rows.Any() ? rows.CopyToDataTable() : table.Clone();
		}

		public static DataSet SetTableNames(this DataSet dataSet, params string[] tableNames)
		{
			for (int index = 0; index < tableNames.Length; index++)
			{
				if (dataSet.Tables.Count > index)
				{
					dataSet.Tables[index].TableName = tableNames[index];
				}
			}

			return dataSet;
		}

		public static DataRow FirstOrNewRow(this DataTable dataTable)
			=> dataTable.Rows.Count > 0 ? dataTable.Rows[0] : dataTable.NewRow();
	}
}
