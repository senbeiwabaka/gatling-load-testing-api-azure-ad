package items

import scala.concurrent.duration._
import scala.concurrent.{ExecutionContext, Future}

import io.gatling.core.Predef._
import io.gatling.http.Predef._

import scala.language.postfixOps
import scalaj.http.{Http, HttpOptions}

import org.json4s._
import org.json4s.native.JsonMethods._

class GetPerformance extends Simulation {
  // Default setup
  val defaultThinkTime = Integer.getInteger("defaultThinkTime", 5)
  val userCount = Integer.getInteger("userCount", 1)
  val rampupDuration = java.lang.Long.getLong("rampupDuration", 0L)
  val constantLoadDuration = java.lang.Long.getLong("constantLoadDuration", 30L)
  val baseURL = System.getProperty(
    "baseURL",
    "https://localhost:7132"
  )
  val tenantId = System.getProperty("tenantId")
  val clientId = System.getProperty("clientId")
  val clientSecret = System.getProperty("clientSecret")
  val scope = System.getProperty("scope")

  var accessToken: String = ""

  // happens once before the simulation actually runs
  before {
    // send a 'POST' request to the token url with the xml/encoded values to get a token
    val result = Http(
      "https://login.microsoftonline.com/" + tenantId
        .toString() + "/oauth2/v2.0/token"
    )
      .postForm(
        Seq(
          "client_id" -> clientId,
          "client_secret" -> clientSecret,
          "grant_type" -> "client_credentials",
          "scope" -> scope
        )
      )
      .asString

    implicit val formats: Formats = DefaultFormats

    // parse the result and path find to the token
    val other = parse(result.body) \ "access_token"

    // copy the token out as a string
    accessToken = other.extract[String]

    println("access token " + accessToken)
  }

  var httpProtocol = http
    .baseUrl(baseURL)
    // .authorizationHeader(s"Bearer $accessToken")

  var scn = scenario("Random")
    .exec { session =>
      session.set("access_token", accessToken)
    }
    .exec(
      http("GETPerformance")
        .get(
          "/api/performance/1"
        )
        .header(
          "Authorization",
          "Bearer ${access_token}"
        )
        .check(
          status.is(200)
        )
    )

  setUp(
    scn
      .inject(
        rampConcurrentUsers(0) to (userCount) during (rampupDuration seconds),
        constantConcurrentUsers(userCount) during (constantLoadDuration seconds)
      )
      .protocols(httpProtocol)
  )
}
