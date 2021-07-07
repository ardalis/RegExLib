using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RegExLib.Data;

/// <summary>
/// Summary description for Author
/// </summary>
public class Author : Object // BaseDataObject, IDataObject 
{

	public Author()
	{
	}

	private Guid id;

	public Guid Id
	{
		get
		{
			return id;
		}
		set
		{
			id = value;
		}
	}


	private String fullName;

	public String FullName
	{
		get
		{
			return fullName;
		}
		set
		{
			fullName = value;
		}
	}




	//public override string DependsOnTable
	//{
	//    get
	//    {
	//        return "dbo.rxl_Author";
	//    }
	//}

	//private string _firstName = String.Empty;
	//public string FirstName
	//{
	//    get
	//    {
	//        return _firstName;
	//    }
	//    set
	//    {
	//        if ( value == _firstName )
	//            return;
	//        _firstName = value;
	//        this.IsDirty = true;
	//    }
	//}

	//private string _lastName = String.Empty;
	//public string LastName
	//{
	//    get
	//    {
	//        return _lastName;
	//    }
	//    set
	//    {
	//        if ( value == _lastName )
	//            return;
	//        _lastName = value;
	//        this.IsDirty = true;
	//    }
	//}

	//public override void Fill( IDataReader reader )
	//{
	//    _firstName = reader.GetString( 2 ); // mandatory field
	//    _lastName = reader.GetString( 3 ); // mandatory field
	//    base.Fill( reader );
	//}
}
