﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TAMIntegra.MessageHandlers
{
    public class APIKeyHandler: DelegatingHandler
    {
        //set a default API key 
        private const string yourApiKey = "90763653eef246a8147e2ba734ba7ab8"; //TamAviacaoExecutiva (MD5)

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool isValidAPIKey = false;
            IEnumerable<string> lsHeaders;
            //Validate that the api key exists

            var checkApiKeyExists = request.Headers.TryGetValues("API_KEY", out lsHeaders);

            if (checkApiKeyExists)
            {
                if (lsHeaders.FirstOrDefault().Equals(yourApiKey))
                {
                    isValidAPIKey = true;
                }
            }

            //If the key is not valid, return an http status code.
            if (!isValidAPIKey)
                return request.CreateResponse(HttpStatusCode.Forbidden, "Chave de autenticação inválida!");

            //Allow the request to process further down the pipeline
            var response = await base.SendAsync(request, cancellationToken);

            //Return the response back up the chain
            return response;
        }
    }
}