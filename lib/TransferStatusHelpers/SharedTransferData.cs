//------------------------------------------------------------------------------
// <copyright file="SharedTransferData.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation
// </copyright>
//------------------------------------------------------------------------------
namespace Microsoft.WindowsAzure.Storage.DataMovement
{
    using System;
    using System.Collections.Concurrent;

    internal class SharedTransferData
    {
        /// <summary>
        /// Gets or sets length of source.
        /// </summary>
        public long TotalLength { get; set; }
        
        /// <summary>
        /// Gets or sets the job instance representing the transfer.
        /// </summary>
        public TransferJob TransferJob { get; set; }

        /// <summary>
        /// Gest or sets list of available transfer data from source.
        /// </summary>
        public ConcurrentDictionary<long, TransferData> AvailableData { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether should disable validation of content md5.
        /// The reader should get this value from source's <c>RequestOptions</c>,
        /// the writer should do or not do validation on content md5 according to this value.
        /// </summary>
        public bool DisableContentMD5Validation { get; set; }

        /// <summary>
        /// Gets or sets string which representing source location.
        /// </summary>
        public string SourceLocation { get; set; }

        /// <summary>
        /// Gets or sets attribute for blob/azure file.
        /// </summary>
        public Attributes Attributes { get; set; }
    }
}
