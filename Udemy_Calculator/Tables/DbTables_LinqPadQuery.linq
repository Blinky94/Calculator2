<Query Kind="Statements">
  <Connection>
    <ID>41e222fa-d0bf-4583-9a45-d8b1a4104781</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AttachFileName>D:\Dev WPF\Udemy_Calculator\Udemy_Calculator\bin\Debug\Calculator.db</AttachFileName>
    <DriverData>
      <EFProvider>Microsoft.EntityFrameworkCore.Sqlite</EFProvider>
    </DriverData>
  </Connection>
</Query>

var formulas = from c in Formulas select c;
formulas.Dump();
var chunks = from c in Chunks select c;
chunks.Dump();
var debugs = from c in Debugs select c;
debugs.Dump();