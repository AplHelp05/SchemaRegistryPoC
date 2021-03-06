// ------------------------------------------------------------------------------
// <auto-generated>
//    Generated by avrogen, version 1.11.0.0
//    Changes to this file may cause incorrect behavior and will be lost if code
//    is regenerated
// </auto-generated>
// ------------------------------------------------------------------------------
namespace schema
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using Avro;
	using Avro.Specific;
	
	public partial class payment : ISpecificRecord
	{
		public static Schema _SCHEMA = Avro.Schema.Parse("{\"type\":\"record\",\"name\":\"payment\",\"namespace\":\"schema\",\"fields\":[{\"name\":\"departm" +
				"entId\",\"type\":\"string\"},{\"name\":\"paymentFee\",\"type\":{\"type\":\"bytes\",\"logicalType" +
				"\":\"decimal\",\"precision\":18,\"scale\":3}}]}");
		private string _departmentId;
		private Avro.AvroDecimal _paymentFee;
		public virtual Schema Schema
		{
			get
			{
				return payment._SCHEMA;
			}
		}
		public string departmentId
		{
			get
			{
				return this._departmentId;
			}
			set
			{
				this._departmentId = value;
			}
		}
		public Avro.AvroDecimal paymentFee
		{
			get
			{
				return this._paymentFee;
			}
			set
			{
				this._paymentFee = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.departmentId;
			case 1: return this.paymentFee;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.departmentId = (System.String)fieldValue; break;
			case 1: this.paymentFee = (Avro.AvroDecimal)fieldValue; break;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}
