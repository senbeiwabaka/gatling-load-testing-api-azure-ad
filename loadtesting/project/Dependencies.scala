import sbt._

object Dependencies {
  var gatling = Seq(
    "io.gatling.highcharts" % "gatling-charts-highcharts" % "3.6.1" % Test,
    "io.gatling" % "gatling-test-framework" % "3.6.1" % Test,
    "org.scalaj" %% "scalaj-http" % "2.4.2",
    "org.json4s" %% "json4s-native" % "4.0.3"
  )
}
