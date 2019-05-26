// <copyright file="AutoMapperConfig.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.BLL
{
    using System;
    using AutoMapper;
    using Mistletoe.Entities;
    using Mistletoe.Models;
    using NLog;

    /// <summary>
    ///     Auto Mapper Configuration
    /// </summary>
    public class AutoMapperConfig
    {
        /// <summary>
        ///     Register Mapping
        /// </summary>
        public static void RegisterMapping()
        {
            Helper.GlobalLogger.Log(LogLevel.Info, "Entering RegisterMapping");

            try
            {
                Mapper.Initialize(config =>
                {
                    config.CreateMap<string, int>().ConvertUsing(stringValue => Convert.ToInt32(stringValue));
                    config.CreateMap<int, string>().ConvertUsing(intValue => Convert.ToString(intValue));
                    config.CreateMap<Campaign, CampaignModel>().ReverseMap();
                    config.CreateMap<Template, TemplateModel>().ReverseMap();
                    config.CreateMap<User, UserModel>().ReverseMap();
                    config.CreateMap<Email_Address, EmailAddressModel>().ReverseMap();
                    config.CreateMap<Template_Email_Addresses, TemplateEmailAddressesModel>().ReverseMap();
                });
            }
            catch (Exception exception)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "RegisterMapping:Error:" + exception.Message);
            }
        }
    }
}