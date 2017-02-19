/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   IUser.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-04-06
 *   职    责   ：   规范流程环节中用户的行为
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-04-06        1.0.0.0        余树杰        初版　 
 *    
 *
 *   
 *   
 *******************************************************************************/

using System;
using System.Collections.Generic;
using Workflow.Platform.Common.Entities;
using System.Configuration;

namespace Workflow.Platform.Common.Interface
{
    public interface IUser
    {
        UserInfo GetUserByUserId(string userID);

        /// <summary>
        /// 根据角色ID获取用户信息
        /// </summary>
        /// <param name="roleID">单个或多个RoleID，多个RoleID以"RoleID1,RoleID2,RoleID3..."格式传递</param>
        /// <returns></returns>
        List<UserInfo> GetUserByRoleID(string roleID);

        List<UserInfo> GetAllUser();
    }
}
