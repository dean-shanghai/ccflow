using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// 属性列表
	/// </summary>
    public class EntityTreeAttr
    {
        /// <summary>
        /// 名称
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// 父节编号
        /// </summary>
        public const string PID = "PID";
        /// <summary>
        /// 树结构编号
        /// </summary>
        public const string ID = "ID";
        /// <summary>
        /// 树编号
        /// </summary>
        public const string TreeNo = "TreeNo";
        /// <summary>
        /// 顺序号
        /// </summary>
        public const string Idx = "Idx";
        /// <summary>
        /// 是否是目录
        /// </summary>
        public const string IsDir = "IsDir";
    }
	/// <summary>
	/// 具有编号名称的基类实体
	/// </summary>
    abstract public class EntityTree : Entity
    {
        #region 属性
        /// <summary>
        /// 唯一标示
        /// </summary>
        public string ID
        {
            get
            {
                return this.GetValStringByKey(EntityTreeAttr.ID);
            }
            set
            {
                this.SetValByKey(EntityTreeAttr.ID, value);
            }
        }
        /// <summary>
        /// 树结构编号
        /// </summary>
        public string TreeNo
        {
            get
            {
                return this.GetValStringByKey(EntityTreeAttr.TreeNo);
            }
            set
            {
                this.SetValByKey(EntityTreeAttr.TreeNo, value);
            }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(EntityTreeAttr.Name);
            }
            set
            {
                this.SetValByKey(EntityTreeAttr.Name, value);
            }
        }
        /// <summary>
        /// 父节点编号
        /// </summary>
        public string PID
        {
            get
            {
                return this.GetValStringByKey(EntityTreeAttr.PID);
            }
            set
            {
                this.SetValByKey(EntityTreeAttr.PID, value);
            }
        }
        /// <summary>
        /// 是否是目录
        /// </summary>
        public bool IsDir
        {
            get
            {
                return this.GetValBooleanByKey(EntityTreeAttr.IsDir);
            }
            set
            {
                this.SetValByKey(EntityTreeAttr.IsDir, value);
            }
        }
        /// <summary>
        /// 顺序号
        /// </summary>
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(EntityTreeAttr.Idx);
            }
            set
            {
                this.SetValByKey(EntityTreeAttr.Idx, value);
            }
        }
        /// <summary>
        /// 级别
        /// </summary>
        public int Grade
        {
            get
            {
                return this.TreeNo.Length / 2;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 主键
        /// </summary>
        public override string PK
        {
            get
            {
                return EntityTreeAttr.ID;
            }
        }
        /// <summary>
        /// 树结构编号
        /// </summary>
        public EntityTree()
        {
        }
        /// <summary>
        /// 树结构编号
        /// </summary>
        /// <param name="_no"></param>
        public EntityTree(string _id)
        {
            if (string.IsNullOrEmpty(_id))
                throw new Exception(this.EnDesc + "@对表[" + this.EnDesc + "]进行查询前必须指定编号。");

            this.ID = _id;
            if (this.Retrieve() == 0)
            {
                throw new Exception("@没有" + this._enMap.PhysicsTable + ", No = " + this.ID + "的记录。");
            }
        }
        #endregion

        #region 业务逻辑处理
        /// <summary>
        /// 重新设置treeNo
        /// </summary>
        public void ResetTreeNo()
        {
        }
        /// <summary>
        /// 检查名称的问题.
        /// </summary>
        /// <returns></returns>
        protected override bool beforeInsert()
        {
            if (this.EnMap.IsAllowRepeatName == false)
            {
                if (this.PKCount == 1)
                {
                    if (this.ExitsValueNum("Name", this.Name) >= 1)
                        throw new Exception("@插入失败[" + this.EnMap.EnDesc + "] 编号[" + this.ID + "]名称[" + Name + "]重复.");
                }
            }
            return base.beforeInsert();
        }
        protected override bool beforeUpdate()
        {
            if (this.EnMap.IsAllowRepeatName == false)
            {
                if (this.PKCount == 1)
                {
                    if (this.ExitsValueNum("Name", this.Name) >= 2)
                        throw new Exception("@更新失败[" + this.EnMap.EnDesc + "] 编号[" + this.ID + "]名称[" + Name + "]重复.");
                }
            }
            return base.beforeUpdate();
        }
        #endregion

        #region 可让子类调用的方法
        /// <summary>
        /// 新建同级节点
        /// </summary>
        /// <returns></returns>
        public string DoCreateSameLevelNode()
        {
            EntityTree en = this.CreateInstance() as EntityTree;
            en.ID = en.GenerNewNoByKey(EntityTreeAttr.ID);
            en.Name = "新建节点" + en.ID;
            en.PID = this.PID;
            en.IsDir = false;
            // en.TreeNo=this.GenerNewNoByKey(EntityTreeAttr.TreeNo,EntityTreeAttr.PID,this.PID)
            en.TreeNo = this.GenerNewNoByKey(EntityTreeAttr.TreeNo,EntityTreeAttr.PID, this.PID);
            en.Insert();
            return en.ID;
        }
        /// <summary>
        /// 新建子节点
        /// </summary>
        /// <returns></returns>
        public string DoCreateSubNode()
        {
            EntityTree en = this.CreateInstance() as EntityTree;
            en.ID = en.GenerNewNoByKey(EntityTreeAttr.ID);
            en.Name = "新建节点" + en.ID;
            en.PID = this.ID;
            en.IsDir = false;
            en.TreeNo = this.GenerNewNoByKey(EntityTreeAttr.TreeNo, EntityTreeAttr.PID, this.ID);
            if (en.TreeNo.Substring(en.TreeNo.Length - 2) == "01")
                en.TreeNo = this.TreeNo + "01";
            en.Insert();

            // 设置此节点是目录
            if (this.IsDir == false)
            {
                this.IsDir = true;
                this.Update(EntityTreeAttr.IsDir, true);
            }
            return en.ID;
        }
        /// <summary>
        /// 上移
        /// </summary>
        /// <returns></returns>
        public string DoUp()
        {
            this.DoOrderUp(EntityTreeAttr.PID, this.PID, EntityTreeAttr.Idx);
            return null;
        }
        /// <summary>
        /// 下移
        /// </summary>
        /// <returns></returns>
        public string DoDown()
        {
            this.DoOrderDown(EntityTreeAttr.PID, this.PID, EntityTreeAttr.Idx);
            return null;
        }
        #endregion
    }
	/// <summary>
    /// 具有编号名称的基类实体s
	/// </summary>
    abstract public class EntitiesTree : Entities
    {
        /// <summary>
        /// 获取它的子节点
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public EntitiesTree GenerHisChinren(EntityTree en)
        {
            EntitiesTree ens = this.CreateInstance() as EntitiesTree;
            foreach (EntityTree item in ens)
            {
                if (en.PID == en.ID)
                    ens.AddEntity(item);
            }
            return ens;
        }
        /// <summary>
        /// 根据位置取得数据
        /// </summary>
        public new EntityTree this[int index]
        {
            get
            {
                return (EntityTree)this.InnerList[index];
            }
        }
        /// <summary>
        /// 构造
        /// </summary>
        public EntitiesTree()
        {
        }
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAll()
        {
            return base.RetrieveAll("TreeNo");
        }
    }
}
