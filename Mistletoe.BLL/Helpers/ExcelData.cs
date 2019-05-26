// <copyright file="ExcelData.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.BLL.Helpers
{
    using System.Collections.Generic;

    /// <summary>
    ///     Excel Data
    /// </summary>
    public class ExcelData
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExcelData"/> class.
        /// </summary>
        public ExcelData()
        {
            this.Receiver = string.Empty;
            this.Placeholders = new List<string>();
        }

        /// <summary>
        ///     Gets or sets receiver Email
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        ///     Gets or sets placeholders for Email
        /// </summary>
        public IList<string> Placeholders { get; set; }
    }
}