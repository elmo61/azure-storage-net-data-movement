//------------------------------------------------------------------------------
// <copyright file="SetContentTypeTest.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation
// </copyright>
//------------------------------------------------------------------------------
namespace DMLibTest.Cases
{
    using DMLibTestCodeGen;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.WindowsAzure.Storage.DataMovement;
    using MS.Test.Common.MsTestLib;

    [MultiDirectionTestClass]
    public class SetContentTypeTest : DMLibTestBase
#if DNXCORE50
        , System.IDisposable
#endif
    {
        #region Initialization and cleanup methods
#if DNXCORE50
        public SetContentTypeTest()
        {
            MyTestInitialize();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            MyTestCleanup();
        }
#endif
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            Test.Info("Class Initialize: SetContentTypeTest");
            DMLibTestBase.BaseClassInitialize(testContext);
        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            DMLibTestBase.BaseClassCleanup();
        }

        [TestInitialize()]
        public void MyTestInitialize()
        {
            base.BaseTestInitialize();
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            base.BaseTestCleanup();
        }
        #endregion

        [TestCategory(Tag.Function)]
        [DMLibTestMethodSet(DMLibTestMethodSet.LocalSource)]
        public void TestSetContentType()
        {
            string contentType = "contenttype/subtype";
            DMLibDataInfo sourceDataInfo = new DMLibDataInfo(string.Empty);
            DMLibDataHelper.AddOneFile(sourceDataInfo.RootNode, DMLibTestBase.FileName, 1024);

            var options = new TestExecutionOptions<DMLibDataInfo>();
            options.TransferItemModifier = (fileNode, transferItem) =>
            {
                UploadOptions uploadOptions = new UploadOptions();
                uploadOptions.ContentType = contentType;

                transferItem.Options = uploadOptions;
            };

            var result = this.ExecuteTestCase(sourceDataInfo, options);

            Test.Assert(result.Exceptions.Count == 0, "Verify no exception is thrown.");
            Test.Assert(DMLibDataHelper.Equals(sourceDataInfo, result.DataInfo), "Verify transfer result.");

            FileNode destFileNode = result.DataInfo.RootNode.GetFileNode(DMLibTestBase.FileName);
            Test.Assert(contentType.Equals(destFileNode.ContentType), "Verify content type: {0}, expected {1}", destFileNode.ContentType, contentType);
        }

        [TestCategory(Tag.Function)]
        [DMLibTestMethodSet(DMLibTestMethodSet.DirLocalSource)]
        public void TestDirectorySetContentType()
        {
            string contentType = "contenttype/subtype";
            DMLibDataInfo sourceDataInfo = new DMLibDataInfo(string.Empty);
            int[] fileSizes = new int[] {1024, 1024, 1024 };
            DMLibDataHelper.AddMultipleFilesDifferentSize(sourceDataInfo.RootNode, DMLibTestBase.FileName, fileSizes);

            var options = new TestExecutionOptions<DMLibDataInfo>();
            options.IsDirectoryTransfer = true;
            options.TransferItemModifier = (fileNode, transferItem) =>
            {
                dynamic uploadOptions = DefaultTransferDirectoryOptions;
                uploadOptions.ContentType = contentType;

                transferItem.Options = uploadOptions;
            };

            var result = this.ExecuteTestCase(sourceDataInfo, options);

            Test.Assert(result.Exceptions.Count == 0, "Verify no exception is thrown.");
            Test.Assert(DMLibDataHelper.Equals(sourceDataInfo, result.DataInfo), "Verify transfer result.");

            foreach (FileNode destFileNode in result.DataInfo.RootNode.FileNodes)
            {
                Test.Assert(contentType.Equals(destFileNode.ContentType), "Verify content type: {0}, expected {1}", destFileNode.ContentType, contentType);
            }
        }
    }
}
