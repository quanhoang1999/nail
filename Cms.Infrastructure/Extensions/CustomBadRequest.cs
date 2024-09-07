using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Infrastructure.Extensions
{
    public class CustomBadRequest: ValidationProblemDetails
    {
        public object Results { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public object Error { get; set; }
        public CustomBadRequest(ActionContext context)
        {
            ConstructErrorMessages(context);
            Status = 400;
            Success = false;
            Results = null;
            Message = null;
            Error = null;
        }
        private void ConstructErrorMessages(ActionContext context)
        {
            foreach (var keyModelStatePair in context.ModelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    if (errors.Count == 1)
                    {
                        var errorMessage = GetErrorMessage(errors[0]);
                        Errors.Add(key, new[] { errorMessage });
                    }
                    else
                    {
                        var errorMessages = new string[errors.Count];
                        for (var i = 0; i < errors.Count; i++)
                        {
                            errorMessages[i] = GetErrorMessage(errors[i]);
                        }

                        Errors.Add(key, errorMessages);
                    }
                }
            }
        }

        string GetErrorMessage(ModelError error)
        {
            return string.IsNullOrEmpty(error.ErrorMessage) ?
                "The input was not valid." : error.ErrorMessage;
        }
    }

}
