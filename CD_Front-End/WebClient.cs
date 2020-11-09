using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace CD_Front_End
{
    public class WebClient
    {
        // Initialize a new HttpClient
        public static HttpClient ApiClient = new HttpClient();

        /// <summary>
        /// Instantiate a new Api Client
        /// </summary>
        static WebClient()
        {
            // Initialize a URI with the WebApiUrl
                // Sets the WebClients connection to the backend as per the WebApiUrl contained in the Web.Config
            ApiClient.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"]);
            // Clear the DefaultRequestHeaders
            ApiClient.DefaultRequestHeaders.Clear();
            // Set the Media type as application/json
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Generic class to handle HTTP Requests
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static class ApiRequest<T>
        { 
            /// <summary>
            /// Enumerate through all items from API result
            /// </summary>
            /// <param name="apiControllerName"></param>
            /// <returns></returns>
        public static IEnumerable<T> GetEnumerable(string apiControllerName)
            {
                // Sends a get request to  retrieve a collection of a specified data set from the defined Api Controller
                return WebClient.ApiClient.GetAsync(apiControllerName).Result.Content.ReadAsAsync<IEnumerable<T>>().Result;
            }

            /// <summary>
            /// Get IList to list all items from the API Result
            /// </summary>
            /// <param name="apiControllerName"></param>
            /// <returns></returns>
            public static IList<T> GetList(string apiControllerName)
            {
                // Sends a  GET request to the Api Controller to retrieve a collection of objects that can be individually accessed by index
                return ApiClient.GetAsync(apiControllerName).Result.Content.ReadAsAsync<IList<T>>().Result;
            }

            /// <summary>
            /// Get a singular item from the API result
            /// </summary>
            /// <param name="apiControllerNameWithID"></param>
            /// <returns></returns>
            public static T GetSingleRecord(string apiControllerNameWithID)
            {
                //Creates a message that sends a GET request to the designated controller
                HttpResponseMessage res = ApiClient.GetAsync(apiControllerNameWithID).Result;
                // Returns the result based off the input<T>
                return res.Content.ReadAsAsync<T>().Result;
            }

            /// <summary>
            /// Create a new record in the database using an object model
            /// </summary>
            /// <param name="apiControllerName">The name of the ApiController</param>
            /// <param name="model">The model that will be used to create a new record</param>
            /// <returns><typeparamref name="T"/> Returns the newly created record as an object model</returns>
            public static T Post(string apiControllerName, T model)
            {
                // Creates a variable to handle a message request to post data (JSON type data) according to a defined ApiControllerName and model
                HttpResponseMessage res = ApiClient.PostAsJsonAsync(apiControllerName, model).Result;
                // Return the Post according to the input<t>
                return res.Content.ReadAsAsync<T>().Result;
            }

            /// <summary>
            /// Will update an existing record based on the parameter Id and the object model
            /// </summary>
            /// <param name="apiControllerNameWithId">The name of the ApiController</param>
            /// <param name="model">The model that will be used to create a new record</param>
            /// <returns>Returns whether the update is successful or not</returns>
            public static bool Put(string apiControllerNameWithId, T model)
            {
                // Creates a variable to handle a message request to put data (JSON type data) according to a defined ApiControllerName and model
                HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync(apiControllerNameWithId, model).Result;
                // Return the response status
                return response.IsSuccessStatusCode;
            }

            /// <summary>
            /// Will delete the record based on the ID
            /// </summary>
            /// <param name="apiControllerNameWithId">The name of the ApiController</param>
            /// <returns>Returns an object model of the deleted record</returns>
            public static T Delete(string apiControllerNameWithId)
            {
                // Create a variable to send a delete request to the controller
                HttpResponseMessage response = WebClient.ApiClient.DeleteAsync(apiControllerNameWithId).Result;
                // Returns the tasks result
                return response.Content.ReadAsAsync<T>().Result;
            }

        }
    }
}