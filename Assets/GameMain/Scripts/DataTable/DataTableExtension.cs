using GameFramework;
using System;
using GameFramework.DataTable;
using UnityGameFramework.Runtime;

namespace MetalMax
{
    /// <summary>
    /// 数据库扩展
    /// </summary>
    public static class DataTableExtension
    {
        private const string DataRowClassPrefixName = "MetalMax.DR";
        
        /// <summary>
        /// 字段分隔符
        /// </summary>
        private static readonly string[] ColumnSplit = new string[] { "," };
        
        /// <summary>
        /// 单个字段的值分隔符
        /// </summary>
        private static readonly string[] ValueSeparator = new string[] { "|" };
        
        /// <summary>
        /// 加载数据表
        /// </summary>
        /// <param name="dataTableComponent">数据表组件</param>
        /// <param name="dataTableName">数据表名称</param>
        /// <param name="userData"></param>
        public static void LoadDataTable(this DataTableComponent dataTableComponent, string dataTableName, object userData = null) 
        {
            if (string.IsNullOrEmpty(dataTableName)) {
                Log.Warning ("Data table name is invalid.");
                return;
            }

            string[] splitNames = dataTableName.Split('_');
            if (splitNames.Length > 2) {
                Log.Warning ("Data table name is invalid.");
                return;
            }

            string dataRowClassName = DataRowClassPrefixName + splitNames[0];

            Type dataRowType = Type.GetType(dataRowClassName);
            if (dataRowType == null) {
                Log.Warning ("Can not get data row type with class name '{0}'.", dataRowClassName);
                return;
            }

            string dataTableNameInType = splitNames.Length > 1 ? splitNames[1] : null;
            dataTableComponent.LoadDataTable(dataRowType, dataTableName, dataTableNameInType, AssetUtility.GetDataTableAsset(dataTableName), userData);
        }

        /// <summary>
        /// 拆分一行数据
        /// </summary>
        /// <param name="dataRowText">一行数据的字符串</param>
        /// <returns>拆分后的数组</returns>
        public static string[] SplitDataRow(string dataRowText) {
            return dataRowText.Split(ColumnSplit, StringSplitOptions.None);
        }
        
        /// <summary>
        /// 拆分一个数据
        /// </summary>
        /// <param name="valueText"></param>
        /// <returns></returns>
        public static string[] SplitValue(string valueText)
        {
            return valueText.Split(ValueSeparator, StringSplitOptions.None);
        }

        /// <summary>
        /// 获取一行数据
        /// </summary>
        /// <param name="id">数据表行的编号</param>
        /// <typeparam name="T">数据表类型</typeparam>
        /// <returns></returns>
        public static T GetDataRow<T>(this DataTableComponent dataTableComponent, int id) where T : IDataRow 
        {
            IDataTable<T> dt = dataTableComponent.GetDataTable<T> ();
            return dt.GetDataRow(id);
        }
        
        
    }
}