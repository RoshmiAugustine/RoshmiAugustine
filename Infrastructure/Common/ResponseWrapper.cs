// -----------------------------------------------------------------------
// <copyright file="ResponseWrapper.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using AutoWrapper;

namespace Opeeka.PICS.Infrastructure.Common
{
    public class ResponseWrapper
    {
        [AutoWrapperPropertyMap(Prop.Result)]
        public object Result { get; set; }
        [AutoWrapperPropertyMap(Prop.ResponseException)]
        public string Error { get; set; }
    }

    public class Error
    {
        public string Message { get; set; }

        public string Code { get; set; }

        public Error(object message, string code)
        {
            this.Message = message.ToString();
            this.Code = code;
        }

    }
}