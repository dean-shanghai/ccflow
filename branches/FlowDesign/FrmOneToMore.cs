using System;
using System.Runtime.Remoting;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BP.En;
using BP.WF;
using BP.Port;
//using BP.PubEn;


namespace BP.Win.WF
{
	/// <summary>
	/// FrmOneToMore 的摘要说明。
	/// </summary>
	public class FrmOneToMore : WFForm
	{
		private BP.Win.Controls.Btn btnSub;
		private BP.Win.Controls.GB gb1;
		private BP.Win.Controls.Btn btnCancel;
		private BP.Win.Controls.Btn btnSave;
		public BP.Win.Controls.Tree tree1;
		public BP.Win.Controls.Tree tree2;
		public BP.Win.Controls.Btn btnNext;
		public BP.Win.Controls.Lab lab1;
		public BP.Win.Controls.Lab lab2;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmOneToMore()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.tree2 = new BP.Win.Controls.Tree();
            this.btnSub = new BP.Win.Controls.Btn();
            this.gb1 = new BP.Win.Controls.GB();
            this.btnCancel = new BP.Win.Controls.Btn();
            this.btnSave = new BP.Win.Controls.Btn();
            this.tree1 = new BP.Win.Controls.Tree();
            this.btnNext = new BP.Win.Controls.Btn();
            this.lab1 = new BP.Win.Controls.Lab();
            this.lab2 = new BP.Win.Controls.Lab();
            this.SuspendLayout();
            // 
            // tree2
            // 
            this.tree2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tree2.CheckBoxes = true;
            this.tree2.HideSelection = false;
            this.tree2.HotTracking = true;
            this.tree2.Location = new System.Drawing.Point(199, 22);
            this.tree2.Name = "tree2";
            this.tree2.ReadOnly = false;
            this.tree2.Size = new System.Drawing.Size(224, 312);
            this.tree2.TabIndex = 0;
            this.tree2.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tree2_AfterCheck);
            this.tree2.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.tree2_BeforeCheck);
            // 
            // btnSub
            // 
            this.btnSub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSub.Location = new System.Drawing.Point(291, 344);
            this.btnSub.Name = "btnSub";
            this.btnSub.Size = new System.Drawing.Size(62, 23);
            this.btnSub.TabIndex = 115;
            this.btnSub.Text = "确定";
            this.btnSub.Click += new System.EventHandler(this.btnSub_Click);
            // 
            // gb1
            // 
            this.gb1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb1.Location = new System.Drawing.Point(0, 332);
            this.gb1.Name = "gb1";
            this.gb1.Size = new System.Drawing.Size(429, 8);
            this.gb1.TabIndex = 112;
            this.gb1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(363, 344);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 23);
            this.btnCancel.TabIndex = 117;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(0, 344);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(64, 23);
            this.btnSave.TabIndex = 113;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tree1
            // 
            this.tree1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tree1.HideSelection = false;
            this.tree1.HotTracking = true;
            this.tree1.Location = new System.Drawing.Point(0, 22);
            this.tree1.Name = "tree1";
            this.tree1.ReadOnly = false;
            this.tree1.Size = new System.Drawing.Size(193, 312);
            this.tree1.TabIndex = 118;
            this.tree1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree1_AfterSelect);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNext.Location = new System.Drawing.Point(72, 344);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(64, 23);
            this.btnNext.TabIndex = 113;
            this.btnNext.Text = "Next";
            this.btnNext.Visible = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lab1
            // 
            this.lab1.AutoSize = true;
            this.lab1.Location = new System.Drawing.Point(0, 8);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(29, 12);
            this.lab1.TabIndex = 119;
            this.lab1.Text = "属性";
            // 
            // lab2
            // 
            this.lab2.AutoSize = true;
            this.lab2.Location = new System.Drawing.Point(204, 8);
            this.lab2.Name = "lab2";
            this.lab2.Size = new System.Drawing.Size(35, 12);
            this.lab2.TabIndex = 120;
            this.lab2.Text = "属性2";
            // 
            // FrmOneToMore
            // 
            this.AcceptButton = this.btnCancel;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(429, 373);
            this.Controls.Add(this.btnSub);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.tree2);
            this.Controls.Add(this.tree1);
            this.Controls.Add(this.gb1);
            this.Controls.Add(this.lab1);
            this.Controls.Add(this.lab2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOneToMore";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "对应关系";
            this.Load += new System.EventHandler(this.FrmOneToMore_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		
		
		#region 字段成员
		private string Title="";

		private GradeEntitiesNoNameBase HisGrades = null;

		private Entity Tree1TagEn = null;
		private string Tree1TagRefKey ="";

		private Entity Tree2TagEn = null;
		private string Tree2TagRefKey ="";

		private Entities HisRefEns3 = null;
		private string   HisRefKey31 ="";
		private string   HisRefKey32 ="";

		#endregion 字段成员


		#region 私有方法

		private void SetTreeTagVisible( Tree tree , bool visible , string dis) 
		{
			tree.TagVisible = visible;
			if( visible )
				tree.TagPropertyName = dis ;
			else
				tree.TagPropertyName = "";
		}

		private void FillNodesByGrades( TreeNodeCollection nds )
		{
			if(nds.Count==0)
				return;
			else
			{
				foreach(TreeNode nd in nds )
				{
					GradeEntityNoNameBase g = nd.Tag as GradeEntityNoNameBase;
					if( g == null )
						continue;

					GradeEntitiesNoNameBase gs = this.HisGrades;
					gs.Clear();

					QueryObject qo =new QueryObject( gs );

					qo.AddWhere( "No" ," like ", g.No +"%" );
					qo.addAnd();
					qo.AddWhere( "Grade", g.Grade+1 );
					qo.DoQuery();

					this.tree2.AddNodes( gs ,nd ," " ,"Name" ,0,2 , true);
					nd.ExpandAll();

					FillNodesByGrades( nd.Nodes );
				}
			}
		}

		
		private Entity GetHisRefEn3()
		{
			Entity en = this.HisRefEns3.GetNewEntity;

			en.SetValByKey( this.HisRefKey31 , this.Tree1TagEn.GetValByKey( this.Tree1TagRefKey) );

			return en;
		}

		private void Check(string no ,TreeNodeCollection ns )
		{
			foreach( TreeNode n  in ns )
			{
				if( n.Tag==null || no == null )
				{
					n.Checked = false;
					if( n.Nodes.Count>0 )
						Check( no , n.Nodes );
					continue;
				}
				else
				{
					Entity en = n.Tag as Entity;
					if( en!=null && en.GetType().Equals( this.Tree2TagEn.GetType()))
					{
						this.Tree2TagEn = en;
						string tag = this.Tree2TagEn.GetValStringByKey( this.Tree2TagRefKey );
						if( tag == no)
							n.Checked = true;
					}
					if( n.Nodes.Count>0 )
						Check( no , n.Nodes );
				}
			}
		}
		



		#region 保存
		private void Save()
		{
			if( !this.btnSave.Enabled )
			{
				return;
			}

			if( this.HisRefEns3 !=null &&  this.HisRefKey31 !="" && this.HisRefKey32 !="")
			{
				this.SaveNodes( this.tree2.Nodes );
			}
			if(this.Text[this.Text.Length-1] == '*')
				this.Text = this.Text.Substring(0 ,this.Text.Length-1 );
			this._isChanged = false;
		}
		private void SaveNodes(TreeNodeCollection nds)
		{
			if(nds.Count==0)
				return;
			foreach( TreeNode nd in nds )
			{
				Entity en3 = this.GetHisRefEn3();

				Entity en2 = nd.Tag as Entity;
				if( en2!=null && en2.GetType().Equals( this.Tree2TagEn.GetType()))
				{
					this.Tree2TagEn = en2;
					string tag = this.Tree2TagEn.GetValStringByKey( this.Tree2TagRefKey );
					en3.SetValByKey( this.HisRefKey32 , tag );
					if(nd.Checked)
					{
						en3.Save();
					}
					else
					{
						en3.Delete();
					}
				}
				SaveNodes( nd.Nodes );
			}
		}
		
		#endregion 保存

		#endregion 私有方法


		#region 公共方法

		public void SetBindKey( string title 
			,string refKey1 ,bool refKey1Visible
			,string refKey2 ,bool refKey2Visible			
			,string RefKey31 , string RefKey32 )
		{
			this.Text = title;
			Title    = title;


			Tree1TagRefKey  = refKey1;
			Tree2TagRefKey  = refKey2;
			HisRefKey31 = RefKey31;
			HisRefKey32 = RefKey32;

			this.SetTreeTagVisible( this.tree1 , refKey1Visible , refKey1);
			this.SetTreeTagVisible( this.tree2 , refKey2Visible , refKey2);
		}

		public void FillTree1ByEns( Entities ens1 , string text1 ,bool check1 ,Entity current1)
		{
			this.Tree1TagEn = ens1.GetNewEntity;

			this.tree1.Nodes.Clear();
			this.tree1.AddNodes( ens1 ,null ," ",text1 ,0,2 , false);
			this.tree1.CheckBoxes = check1;

			if(current1 != null)
				foreach( TreeNode n in this.tree1.Nodes )
				{
					Entity en = n.Tag as Entity;
					if( en.GetValStringByKey( this.Tree1TagRefKey ) == current1.GetValStringByKey( this.Tree1TagRefKey))
					{
						this.tree1.SelectedNode =  n ;
					}
				}
		}

		public void FillTree1ByGrades( GradeEntitiesNoNameBase gs)
		{
//			this.Tree1TagEn = gs.GetNewEntity;
//			this.HisGrades = gs;
//			QueryObject qo =new QueryObject( gs );
//			qo.AddWhere(BP.Port.DutyAttr.Grade, 1 );
//			qo.DoQuery();
//
//			this.tree1.Nodes.Clear();
//			this.tree1.AddNodes( gs ,null ," " ,"Name" ,0,2 , true);
//
//			this.FillNodesByGrades( this.tree1.Nodes );
		}


		public void FillTree2ByEns( Entities ens2 , string text2 )
		{
			this.Tree2TagEn = ens2.GetNewEntity;
			this.tree2.Nodes.Clear();
			this.tree2.AddNodes( ens2 ,null ," " ,text2 ,0,2 , false);
		}
		
		public void FillTree2ByGrades( GradeEntitiesNoNameBase gs)
		{
//			this.Tree2TagEn = gs.GetNewEntity;
//			this.HisGrades = gs;
//			QueryObject qo =new QueryObject( gs );
//			qo.AddWhere(BP.Port.DutyAttr.Grade, 1 );
//			qo.DoQuery();
//
//			this.tree2.Nodes.Clear();
//			this.tree2.AddNodes( gs ,null ," " ,"Name" ,0,2 , true);
//			this.Tree2TagRefKey = "No";
//
//			this.FillNodesByGrades( this.tree2.Nodes );
		}
		


		public void FillEnsAndEns( Entities ens1 , string text1 ,bool check1 ,Entity current1 
			,Entities ens2 , string text2 )
		{
			this.FillTree2ByEns( ens2 , text2 );
			this.FillTree1ByEns( ens1 , text1 ,check1 ,current1 );
		}
		

		public void FillGradesAndGrades( GradeEntitiesNoNameBase gs1
			, GradeEntitiesNoNameBase gs2 )
		{
			this.FillTree2ByGrades( gs2 );
			this.FillTree1ByGrades( gs1 );
		}

		public void FillEnsAndGrades( Entities ens1 , string text1 ,bool check1 ,Entity current1 
			, GradeEntitiesNoNameBase gs2 )
		{
			this.FillTree2ByGrades( gs2 );
			this.FillTree1ByEns( ens1 , text1 ,check1 ,current1 );
		}

		public void FillGradesAndEns( GradeEntitiesNoNameBase gs1 
			,Entities ens2 , string text2 )
		{
			this.FillTree2ByEns( ens2 ,text2 );
			this.FillTree1ByGrades( gs1 );
		}

			 
		
		public void BindCheckEns( Entities ens )
		{
			this._isChanged = true;
			this.HisRefEns3 = ens;

			this.Check( null ,this.tree2.Nodes );//set all check false

			if( ens!=null && ens.Count>0)
				foreach( Entity en  in ens )
				{
					object val = en.GetValByKey( HisRefKey32 );
					if( val !=null && val.ToString() != "")
						this.Check( val.ToString() , this.tree2.Nodes );
				}


			this._isChanged = false;
		}

		public  void FillTree1ByEmps()
		{
			this.Tree1TagEn = new Emp();

			Tree tree = this.tree1;
			BP.Port.Depts dps =new BP.Port.Depts();
			dps.RetrieveAll();
			tree.TagVisible = true ;
			tree.TagPropertyName = "No";
			tree.AddNodes( dps,null,"No","Name",0,2,true );

			foreach( TreeNode nd in tree.Nodes )
			{
				Emps ens = new Emps( ); 
				QueryObject qo = new QueryObject( ens );
				qo.AddWhere( EmpAttr.FK_Dept , nd.Tag.ToString());
				qo.DoQuery();
				
				for(int i=0;i<ens.Count ;i++)
				{
					Emp e = ens[i] as Emp;

					TreeNode eNode = new TreeNode( e.Name +"["+ e.No+"]" );
					eNode.Tag =  e ;
					eNode.ImageIndex = 0;
					eNode.SelectedImageIndex = 2;
					nd.Nodes.Add( eNode );
				}
			}

		}
        public void FillTree2ByEmps()
        {
            this.Tree2TagEn = new Emp();

            Tree tree = this.tree2;

            BP.Port.Depts dps = new BP.Port.Depts();
            dps.RetrieveAll();
            tree.TagVisible = true;
            tree.TagPropertyName = "No";
            tree.AddNodes(dps, null, " ", "Name", 0, 2, true);

            foreach (TreeNode nd in tree.Nodes)
            {
                BP.Port.Dept dp = nd.Tag as BP.Port.Dept;

                Emps ens = new Emps();
                En.QueryObject qo = new QueryObject(ens);
                qo.AddWhere(EmpAttr.FK_Dept, dp.No);
                qo.DoQuery();

                for (int i = 0; i < ens.Count; i++)
                {
                    Emp e = ens[i] as Emp;

                    TreeNode eNode = new TreeNode(e.Name + "[" + e.No + "]");
                    eNode.Tag = e;
                    eNode.ImageIndex = 0;
                    eNode.SelectedImageIndex = 2;
                    nd.Nodes.Add(eNode);
                }
            }
            tree.AfterCheck += new TreeViewEventHandler(tree_AfterCheck);
        }	
		private void tree_AfterCheck(object sender, TreeViewEventArgs e)
		{
			Emp em = e.Node.Tag as Emp;
			if( em!=null)
			{
				if( e.Node.Checked )
				{
					TreeNode tmp = e.Node.Parent;
					while( tmp !=null)
					{
						tmp.Checked = true;
						tmp = tmp.Parent;
					}
				}
				else 
					 CheckParentNode( e.Node.Parent );
			}
		}
		private void CheckParentNode( TreeNode parent)
		{
			if( parent==null)
				return ;
			parent.Checked = false;
			foreach( TreeNode ch in parent.Nodes )
			{
				if( ch.Checked )
				{
					parent.Checked = true;
					return;
				}
			}
		}
		public bool ReadOnly
		{
			get
			{
				return !this.btnSave.Enabled;
			}
			set
			{
				this.btnSave.Enabled = !value;
				this.btnSub.Enabled = !value;
			}
		}

		#endregion 公共方法


		#region Btn 事件
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Save();
			}
			catch(Exception ex )
			{
				MessageBox.Show(ex.Message ,"保存失败！");
			}
		}

		private void btnSub_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Save();
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch(Exception ex )
			{
				MessageBox.Show(ex.Message ,"保存失败！");
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

	
		#endregion 事件


		#region Tree 事件

		public event TreeViewEventHandler Tree1AfterSelect ;

		private void tree1_AfterSelect(object sender,TreeViewEventArgs e)
		{
			if(Tree1AfterSelect!=null)
				Tree1AfterSelect( this , e );

			this.Text = this.Title + " ["+ tree1.SelectedNode.Text +"]";
			Entity cur = tree1.SelectedNode.Tag as Entity;
			this.HisRefEns3.Clear();
			if ( cur!=null && cur.GetType().Equals( this.Tree1TagEn.GetType()))
			{
				this.Tree1TagEn = cur;			
				Type tp = this.HisRefEns3.GetType();
				object[] objs = new object[]{ this.Tree1TagEn.GetValByKey( this.Tree1TagRefKey ) };

				Object o=null;

				try
				{
				 
					o = System.Activator.CreateInstance( tp ,objs);
				}
				catch
				{
					BP.WF.NodeStations nds =new BP.WF.NodeStations();
					BP.En.QueryObject qo = new QueryObject(nds);
					qo.AddWhere( NodeStationAttr.FK_Node, this.Tree1TagEn.GetValByKey( this.Tree1TagRefKey ));
					qo.DoQuery();

					o=nds;

				}
				//				}
				//				catch
				//				{
				//					//this.HisRefEns3 = o as Entities;
				//				}
				//if (o!=null)

				this.HisRefEns3 = o as Entities;

				this.tree2.Enabled = true;
			}
			else
			{
				this.tree2.Enabled = false;
			}
			this.BindCheckEns( this.HisRefEns3 );
		}

		//提示未保存
		private bool _isChanged = false;
		private void tree2_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if( !_isChanged)
			{
				if(this.Text[this.Text.Length-1] != '*')
					this.Text +="*";
				_isChanged = true;
			}
		}
		#endregion Tree 事件
	
		
		public void DeteleErrorData()
		{
			string err ="";
			try
			{
				Entity enref = this.HisRefEns3.GetNewEntity;
				string sql = "delete from "+this.HisRefEns3.GetNewEntity.EnMap.PhysicsTable  //Port_dutyStation 
					+" where "
					+ enref.EnMap.GetFieldByKey( this.HisRefKey31 ) 
					+" not in ( select "+ this.Tree1TagEn.EnMap.GetFieldByKey( this.Tree1TagRefKey )
					+" from "+ this.Tree1TagEn.EnMap.PhysicsTable +" )"
					+" or "
					+ enref.EnMap.GetFieldByKey( this.HisRefKey32 ) 
					+" not in ( select "+ this.Tree2TagEn.EnMap.GetFieldByKey( this.Tree2TagRefKey ) 
					+" from "+ this.Tree2TagEn.EnMap.PhysicsTable +" )";
				err ="\nsql:"+ sql;

				DA.DBAccess.RunSQL( sql );
			}
			catch(Exception ex)
			{
				MessageBox.Show( ex.Message + err, "删除无效数据时出错！");
			}
		}

		private void tree2_BeforeCheck(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
		}

        private void btnNext_Click(object sender, EventArgs e)
        {

        }

        private void FrmOneToMore_Load(object sender, EventArgs e)
        {
            this.btnNext.Visible = false;

        }

	
	}
}
