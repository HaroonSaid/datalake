// ------------------------------------------------------------------------------
// <auto-generated>
//    Generated by avrogen, version 1.7.7.5
//    Changes to this file may cause incorrect behavior and will be lost if code
//    is regenerated
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Models
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using global::Avro;
	using global::Avro.Specific;
	
	public partial class UserInfo : ISpecificRecord
	{
		public static Schema _SCHEMA = Schema.Parse("{\"type\":\"record\",\"name\":\"UserInfo\",\"namespace\":\"Models\",\"fields\":[{\"name\":\"First\"" +
				",\"type\":\"string\"},{\"name\":\"Middle\",\"default\":\"\",\"type\":\"string\"},{\"name\":\"Last\"," +
				"\"type\":\"string\"}]}");
		private string _First;
		private string _Middle;
		private string _Last;
		public virtual Schema Schema
		{
			get
			{
				return UserInfo._SCHEMA;
			}
		}
		public string First
		{
			get
			{
				return this._First;
			}
			set
			{
				this._First = value;
			}
		}
		public string Middle
		{
			get
			{
				return this._Middle;
			}
			set
			{
				this._Middle = value;
			}
		}
		public string Last
		{
			get
			{
				return this._Last;
			}
			set
			{
				this._Last = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.First;
			case 1: return this.Middle;
			case 2: return this.Last;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.First = (System.String)fieldValue; break;
			case 1: this.Middle = (System.String)fieldValue; break;
			case 2: this.Last = (System.String)fieldValue; break;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}
