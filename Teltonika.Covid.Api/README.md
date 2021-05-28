## Connection string
This requires two database connection strings to be added to secrets or `appsettings.json`
```
{
  "ConnectionStrings": {
    "usersDb": "Server=<server>;Database=<DB for users>;;User Id=<username>;Password=<password>;",
    "covidDb": "Server=<server>;Database=<DB for data>;;User Id=<username>;Password=<password>;"
  }
}
```