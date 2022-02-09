## testing - performance test suite

Project uses [sbt plugin][sbtplugindoc] of [gatling][gatlingdoc].
It contains basic simulation from gatling quick start bundle.

[sbtplugindoc]: https://gatling.io/docs/current/extensions/sbt_plugin/
[gatlingdoc]: https://gatling.io/docs/current/advanced_tutorial/

### Params
|Param Name                 |Default                    |Description                                                        |
|---------------------------|--------------------------:|-------------------------------------------------------------------|
|defaultThinkTime           |5                          |think time in seconds - which is the delay between user actions    |
|userCount                  |1                          |requested number of users once totally ramped up                   |
|rampupDuration             |0                          |number of seconds for ramp up of users                             |
|constantLoadDuration       |30                         |number of seconds for steady state run once users are ramped up    |
|baseURL                    |https://localhost:7132     |base URL for endpoint being tested                                 |
|tenantId (Required)        |                           |Azure AD tenant id                                                 |
|clientId (Required)        |                           |Azure app registration client id                                   |
|clientSecret (Required)    |                           |Azure app registration client secret                               |
|scope (Required)           |                           |Azure app registration scope                                       |

### Run

All tests:
```
sbt "Gatling/test"
```

Single test:
```
sbt "gatling:testOnly items.GetRandom"
```

Different parameters:
```
sbt -DdefaultThinkTime=3 -DuserCount=10 -DrampupSeconds=10 -DconstantLoadDuration=50 -DbaseURL=http://some.url Gatling/test
```
```
sbt -DtenantId='123' -DclientId='456' -DclientSecret='secret' -Dscope='api://456/.default' Gatling/test
```

Report:
```
sbt "gatling:lastReport"
```
