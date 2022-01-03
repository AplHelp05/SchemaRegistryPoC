### Issue Serializing a message with Schema Registry Benthos

Scenario: We have a Avro Schema with two fields: **DepartmentId** (String) & **PaymentFee** (Logical Type: Decimal), check the `.avsc` schema 
```json
{
    "type": "record", 
    "namespace": "schema.payment",
    "name": "payment", 
    "fields": [
      { "name" : "departmentId" , "type" : "string" },
      {
          "name" : "paymentFee" , 
          "type":{
            "type": "bytes",
            "logicalType": "decimal",
            "precision": 18,
            "scale": 3
          }
      }
    ]
}
```

When we use the `schema_registry_encode` to serialize the message, the following issue is thrown:
```text
Error: cannot encode binary record "payment" field "paymentFee": value does not match its schema: cannot transform to bytes, expected *big.Rat, received json.Number
```
Apparently `PaymentFee` must be of `*Big.Rat` type but it is receiving a Json.Number (check the Benthos producer project)

### Issue Deserializing a message with Schema Registry Benthos
We have a **Dotnet Producer** Solution, which uses the same schema to push a message into Kafka, & a **Benthos Consumer** trying to deserialize the message. 

When Benthos deserializes the message, the value comes in `Null` & if we add `.string()`, the value of the `paymentFee` is `"number/number"` (for example the dotnet producer sent `23.900`, but the Benthos Consumer deserialize it as Null or with `.string()` with `"239/10"`)
```yaml
- bloblang: |-
    root = this
    root.departmentId = this.departmentId
    root.paymentFee = this.paymentFee.string()
```

### Questions
1) Is Benthos able to serialize/contains Messages that contains a Decimal Logical Type?
2) If not, What would be the best approach to serialize/deserialize a message with a `Decimal Logical Type` field or a double without losing precision?