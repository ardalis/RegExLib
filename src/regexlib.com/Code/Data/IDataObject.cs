using System;
public interface IDataObject
{
    string DependsOnTable { get; }
    void Fill(System.Data.IDataReader reader);
}
