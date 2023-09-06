using System;
using System.Collections.Generic;

namespace OneDay.TowerDefense.Base.Systems
{
    public interface ITableItem
    { }

    public interface ITable
    {
        public string Id { get; }
        int GetRowsCount();
    }
    
    public interface ITable<out T> : ITable where T: ITableItem
    {
        T GetByIndex(int index);
        IEnumerable<T> GetRows();
    }
    
    public class DataSystem : ASystem
    {
        private List<ITable> Tables { get; set; } = new();

        public ITable<T> GetTable<T>(string tableId) where T : ITableItem
        {
            var table = Tables.Find(x => x.Id == tableId);
            return (ITable<T>)table;
        }

        public void AddTable(ITable table)
        {
            if (Tables.Find(x => x.Id == table.Id) != null)
            {
                throw new ArgumentException($"Table with id {table.Id} already exists");
            }
            Tables.Add(table);
        }
    }
}