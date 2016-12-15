using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using EDoc2;
using EDoc2.Document;
using EDoc2.FileAgent;
using EDoc2.MetaData;
using EDoc2.Organization;
using EDoc2.Website;
using Microsoft.Win32;

namespace BiostimeProcess.Service.Utitity
{
    public class EDoc2Helper
    {
        public static string Token
        {
            get { return GetAdminToken(); }
        }

        /// <summary>
        ///     获取Token
        /// </summary>
        /// <returns></returns>
        public static string GetAdminToken()
        {
            try
            {
                string token;
                //ApiManager.Api.OrgnizationManagement.Login(account, ipAddress, out token);
                int result = ApiManager.Api.OrgnizationManagement.ImpersonateByLoginName("admin", "127.0.0.1", out token);
                //int result = ApiManager.Api.OrgnizationManagement.Impersonate(2, "127.0.0.1", out token);
                if (result != 0)
                {
                    throw new Exception("GetAdminToken失败,result=" + result);
                }
                return token;
            }
            catch (Exception ex)
            {
                throw new Exception("GetAdminToken出现异常,exMessage=" + ex.Message);
            }
        }

        #region 组织架构

        /// <summary>
        ///     根据部门ID获取所有子部门
        /// </summary>
        /// <param name="deptId">所在部门ID</param>
        /// <returns>所在部门ID下所有部门名称</returns>
        public static IList<string> GetChildDeptNames(int deptId)
        {
            List<EDoc2DepartmentInfo> childDepartments;
            int result = ApiManager.Api.OrgnizationManagement.GetChildDepartments(Token, deptId, out childDepartments);
            if (result != 0)
            {
                throw new Exception("GetChildDeptNames错误,result=" + result);
            }
            if (childDepartments.Count == 0)
            {
                return new List<string>();
            }
            return childDepartments.Select(item => item.DeptName).ToList();
        }

        /// <summary>
        ///     根据用户id获取UserRealName
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>用户名</returns>
        public static string GetUserNameById(int userId)
        {
            string userName = string.Empty;
            EDoc2UserInfo userInfo;
            int result = ApiManager.Api.OrgnizationManagement.GetUserById(Token, userId, out userInfo);
            if (result == 0)
            {
                userName = userInfo.UserRealName;
            }
            return userName;
        }

        /// <summary>
        ///     根据用户id获取UserRealName
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>用户名</returns>
        public static string GetUserNameById(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return string.Empty;
            }
            return GetUserNameById(int.Parse(userId));
        }

        public static string GetCurrentUserRealName()
        {
            try
            {
                EDoc2UserInfo userInfo = WebsiteUtility.CurrentUser;
                return userInfo.UserRealName;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static int GetCurrentUserId()
        {
            try
            {
                EDoc2UserInfo userInfo = WebsiteUtility.CurrentUser;
                return userInfo.UserId;
            }
            catch
            {
                return 0;
            }
        }

        public static int GetCurrentUserDeptId()
        {
            try
            {
                EDoc2UserInfo userInfo = WebsiteUtility.CurrentUser;
                return userInfo.DeptId;
            }
            catch
            {
                return 0;
            }
        }

        public static string GetCurrentUserDeptName()
        {
            try
            {
                EDoc2UserInfo userInfo = WebsiteUtility.CurrentUser;
                return userInfo.DepartmentName;
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        #region 文件夹操作

        /// <summary>
        ///     获取文件夹id,如果文件夹不存在则新建文件夹并返回文件夹id
        /// </summary>
        /// <param name="parentFolderId">父文件夹id</param>
        /// <param name="folderName">文件夹名称</param>
        /// <returns></returns>
        public static int GetFolder(int parentFolderId, string folderName)
        {
            string token = GetAdminToken();
            return GetFolder(token, parentFolderId, folderName);
        }

        /// <summary>
        ///     获取文件夹id,如果文件夹不存在则新建文件夹并返回文件夹id
        /// </summary>
        /// <param name="token">token凭证</param>
        /// <param name="parentFolderId">父文件夹id</param>
        /// <param name="folderName">文件夹名称</param>
        /// <returns>文件夹id</returns>
        public static int GetFolder(string token, int parentFolderId, string folderName)
        {
            try
            {
                folderName = folderName.Trim();
                bool exist;
                int result = ApiManager.Api.DocumentManagement.ExistsFolder(token, parentFolderId, folderName, out exist);
                if (exist)
                {
                    return result;
                }
                int resultInt = CreateFolder(parentFolderId, folderName);
                return resultInt;
            }
            catch (Exception ex)
            {
                throw new Exception("GetFolder出现异常,exMessage=" + ex.Message);
            }
        }

        /// <summary>
        ///     新建文件夹(默认目录名:Guid)
        /// </summary>
        /// <param name="parentFolderId">所在文件夹id</param>
        /// <returns>新文件夹id</returns>
        public static int CreateFolder(int parentFolderId)
        {
            try
            {
                string folderName = Guid.NewGuid().ToString("N");
                return CreateFolder(parentFolderId, folderName);
            }
            catch (Exception ex)
            {
                throw new Exception("新建文件夹失败:" + ex.Message);
            }
        }

        /// <summary>
        ///     新建文件夹
        /// </summary>
        /// <param name="parentFolderId">父文件夹id</param>
        /// <param name="folderName">文件夹名称</param>
        /// <returns>新文件夹id</returns>
        public static int CreateFolder(int parentFolderId, string folderName)
        {
            try
            {
                string token = GetAdminToken();
                IEDoc2Folder folder;
                int result = ApiManager.Api.DocumentManagement.CreateFolder(
                    token, parentFolderId, folderName, string.Empty, 0, 0, 0, string.Empty, string.Empty, 1, out folder);
                if (result != 0)
                {
                    throw new Exception("CreateFolder出错,result=" + result);
                }
                return folder.FolderId;
            }
            catch (Exception ex)
            {
                throw new Exception("CreateFolder出现异常,exMessage=" + ex.Message);
            }
        }

        #endregion

        #region 文件操作

        public delegate string GetAttrValueDelegate(object entity, string attrName);

        /// <summary>
        ///     创建文件
        /// </summary>
        /// <param name="targetFolderId">目标文件夹id</param>
        /// <param name="uploadFileName">文件名称</param>
        /// <param name="fileStream">文件流</param>
        /// <returns>文件id</returns>
        public static int CreateFile(int targetFolderId, string uploadFileName, Stream fileStream)
        {
            int nResult;
            IEDoc2File fileCreated;
            try
            {
                string extension = Path.GetExtension(uploadFileName);
                // 判断文件是否存在
                int count = 1;
                bool exist;
                ApiManager.Api.DocumentManagement.ExistsFile(Token, targetFolderId, uploadFileName, out exist);
                while (exist)
                {
                    string fileWithOutExt = uploadFileName.Substring(
                        0, uploadFileName.LastIndexOf(".", StringComparison.Ordinal));
                    string fileName1 = fileWithOutExt + "(" + count.ToString(CultureInfo.InvariantCulture) + ")" +
                                       uploadFileName.Substring(
                                           uploadFileName.LastIndexOf(".", StringComparison.Ordinal),
                                           uploadFileName.Length - fileWithOutExt.Length);
                    ApiManager.Api.DocumentManagement.ExistsFile(Token, targetFolderId, fileName1, out exist);
                    if (!exist)
                    {
                        uploadFileName = fileName1;
                    }
                    count++;
                }

                nResult = ApiManager.Api.DocumentManagement.CreateFile(Token, targetFolderId, uploadFileName, "", "",
                                                                       extension, GetContentType(extension), 0,
                                                                       fileStream.Length,
                                                                       out fileCreated);
                if (nResult != EDoc2ApiConst.ERROR_SUCCEEDED)
                {
                    throw new Exception("创建文件失败,result=" + nResult);
                }
                try
                {
                    string absolutePath1;
                    ApiManager.Api.StorageManagement.GetAbsolutePath(Token, fileCreated.LastVersion, out absolutePath1);
                    IFileAgent agent = FileAgentForApi.Create(
                        ApiManager.Api, fileCreated.LastVersion.StorageDirectory.StorageServer.RegionId);
                    Stream stream1 =
                        agent.OpenWrite(new PathEnvelope(fileCreated.LastVersion.StorageDirectory.StorageServer,
                                                         absolutePath1));

                    byte[] bytes;
                    int bufSize = 10240;
                    while (fileStream.Position < fileStream.Length)
                    {
                        if (fileStream.Length - fileStream.Position < bufSize)
                        {
                            bufSize = (int) (fileStream.Length - fileStream.Position);
                        }
                        bytes = new byte[bufSize];
                        int read = fileStream.Read(bytes, 0, bufSize);
                        stream1.Write(bytes, 0, read);
                    }
                    stream1.Close();
                    fileCreated.LastVersion.Complete(Token, true, fileStream.Length);

                    fileStream.Close();
                }
                catch (Exception ex)
                {
                    ApiManager.Api.DocumentManagement.UndoCreateFile(Token, fileCreated.FileId);
                    throw new Exception("CreateFile出错,ex.Message=" + ex.Message);
                }
            }
            catch
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
                throw;
            }
            return fileCreated.FileId;
        }

        public static string GetContentType(string ext)
        {
            string defaultValue = "application/x-msdownload";
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(ext);
            if (key == null)
            {
                return defaultValue;
            }
            object obj = key.GetValue("Content Type");
            if (obj == null)
            {
                return defaultValue;
            }
            return obj.ToString();
        }

        /// <summary>
        ///     根据文件id获取文件名称
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <returns>文件名称</returns>
        public static string GetFileNameByFileId(int fileId)
        {
            IEDoc2File resultEDocFileInfo = GetFileInfoByFileId(fileId);
            return resultEDocFileInfo.FileName;
        }

        /// <summary>
        ///     根据文件id获取文件信息
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <returns>ResultEDocFileInfo：Result=0成功,EDocFileInfo:文件信息</returns>
        private static IEDoc2File GetFileInfoByFileId(int fileId)
        {
            IEDoc2File eDoc2File;
            int result = ApiManager.Api.DocumentManagement.GetFileById(Token, fileId, out eDoc2File);
            if (result != 0)
            {
                throw new Exception("GetFileInfoByFileId出错,result=" + result);
            }
            return eDoc2File;
        }

        //private static string FormatFileName(string fileName)
        //{
        //格式化文件名(去除win文件\文件夹不允许的字符)
        //    fileName = fileName.Trim();
        //    char[] charArray = Path.GetInvalidFileNameChars();
        //    foreach (char charVal in charArray)
        //    {
        //        string c = charVal.ToString(CultureInfo.InvariantCulture);
        //        if (fileName.Contains(c))
        //        {
        //            fileName = fileName.Replace(c, "");
        //        }
        //    }
        //    return fileName;
        //}

        /// <summary>
        ///     移动文件
        /// </summary>
        /// <param name="targetFolderId">目标文件夹id</param>
        /// <param name="dropPerms">是否删除权限</param>
        /// <param name="fileId">文件id</param>
        /// <returns></returns>
        public static int MoveFile(int targetFolderId, bool dropPerms, int fileId)
        {
            string token = GetAdminToken();
            //ResultInt:Result=0成功,ResultValue:文件编号
            try
            {
                int[] fileIds = {fileId};
                var folderIds = new int[0];
                return ApiManager.Api.DocumentManagement.Move(token, folderIds, fileIds, targetFolderId, dropPerms);
            }
            catch (Exception ex)
            {
                throw new Exception("MoveFile出现异常,exMessage=" + ex.Message);
            }
        }

        /// <summary>
        ///     改变文件名
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <param name="newFileName">新文件名</param>
        /// <returns></returns>
        public static int ChanageFileName(int fileId, string newFileName)
        {
            return ChanageFileName(GetAdminToken(), fileId, newFileName);
        }

        /// <summary>
        ///     改变文件名
        /// </summary>
        /// <param name="token">token凭证</param>
        /// <param name="fileId">文件id</param>
        /// <param name="newFileName">新文件名</param>
        /// <returns></returns>
        public static int ChanageFileName(string token, int fileId, string newFileName)
        {
            //result=0成功
            try
            {
                int result = ApiManager.Api.DocumentManagement.RenameFile(token, fileId, newFileName, string.Empty);
                if (result != 0)
                {
                    throw new Exception("ChanageFileName出错,result=" + result);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("ChanageFileName出现异常,exMessage=" + ex.Message);
            }
        }

        /// <summary>
        ///     发布文件主板本
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <returns></returns>
        public static int PublishFileVersion(int fileId)
        {
            string token = GetAdminToken();
            try
            {
                IEDoc2File file;
                ApiManager.Api.DocumentManagement.GetFileById(fileId, out file);
                if (file == null)
                {
                    throw new Exception(string.Format("PublishFileVersion错误:找不到id为{0}的文件", fileId));
                }
                int result = file.PublishFileVersion(token);
                if (result != 0 && result != 713)
                {
                    throw new Exception("PublishFileVersion错误:" + result);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("PublishFileVersion出现异常,exMessage=" + ex.Message);
            }
        }

        /// <summary>
        ///     给文件添加元数据
        /// </summary>
        /// <param name="metaValues">元数据属性值对象</param>
        /// <param name="fileId">Edoc2文件ID</param>
        /// <param name="metaTypeId">元数据类型ID</param>
        /// <param name="getAttrValue">元数据属性对应值的方法</param>
        /// <returns></returns>
        public static List<IEDoc2MetaObjType> AddFileMetaByMetaTypeId(
            object metaValues, int fileId, int metaTypeId, GetAttrValueDelegate getAttrValue)
        {
            try
            {
                IEDoc2File file;
                ApiManager.Api.DocumentManagement.GetFileById(fileId, out file);
                List<IEDoc2MetaAttr> metaAttrList;
                ApiManager.Api.MetaDataManagement.GetMetaAttrByTypeId(Token, metaTypeId, out metaAttrList);

                var metaValueList = new List<IEDoc2MetaValue>();
                foreach (IEDoc2MetaAttr attr in metaAttrList)
                {
                    var value = new EDoc2MetaValue();
                    value.AttrId = attr.AttrId;
                    value.AttrPath = metaTypeId + "\\";
                    value.AttrValue = getAttrValue(metaValues, attr.AttrName);
                    value.ObjId = file.FileLastVerId;
                    value.ObjType = 3;

                    metaValueList.Add(value);
                }

                IEDoc2MetaObjType metaObjType = new EDoc2MetaObjType(0, file.FileLastVerId, 3, metaTypeId, metaValueList);
                var edoc2MetaObjTypeList = new List<IEDoc2MetaObjType>();
                edoc2MetaObjTypeList.Add(metaObjType);

                int lastVerId;
                file.CreateSysMetaData(Token, edoc2MetaObjTypeList, out lastVerId);
                file.OverlayPrevVersion(Token);

                return edoc2MetaObjTypeList;
            }
            catch (Exception ex)
            {
                throw new Exception("AddFileMetaByMetaTypeId出现异常,exMessage=" + ex.Message);
            }
        }

        #endregion
    }
}