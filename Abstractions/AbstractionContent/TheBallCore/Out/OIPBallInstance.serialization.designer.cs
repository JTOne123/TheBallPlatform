 

namespace SER.AaltoGlobalImpact.OIP { 
		using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using System.Linq;
using System.Runtime.Serialization;


namespace INT { 
		            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP.INT")]
			public partial class CategoryChildRanking
			{
				[DataMember]
				public string CategoryID { get; set; }
				[DataMember]
				public RankingItem[] RankingItems { get; set; }
			}

            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP.INT")]
			public partial class RankingItem
			{
				[DataMember]
				public string ContentID { get; set; }
				[DataMember]
				public string ContentSemanticType { get; set; }
				[DataMember]
				public string RankName { get; set; }
				[DataMember]
				public string RankValue { get; set; }
			}

            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP.INT")]
			public partial class ParentToChildren
			{
				[DataMember]
				public string id { get; set; }
				[DataMember]
				public ParentToChildren[] children { get; set; }
			}

 }             [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBSystem 
			{

				public TBSystem()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBSystem";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBSystem));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBSystem DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBSystem));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBSystem) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(TBSystem sourceObject)
				{
					InstanceName = sourceObject.InstanceName;
					AdminGroupID = sourceObject.AdminGroupID;
				}
				



			[DataMember]
			public string InstanceName { get; set; }
			private string _unmodified_InstanceName;
			[DataMember]
			public string AdminGroupID { get; set; }
			private string _unmodified_AdminGroupID;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class WebPublishInfo 
			{

				public WebPublishInfo()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "WebPublishInfo";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(WebPublishInfo));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static WebPublishInfo DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(WebPublishInfo));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (WebPublishInfo) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(WebPublishInfo sourceObject)
				{
					PublishType = sourceObject.PublishType;
					PublishContainer = sourceObject.PublishContainer;
					ActivePublication = sourceObject.ActivePublication;
					Publications = sourceObject.Publications;
				}
				



			[DataMember]
			public string PublishType { get; set; }
			private string _unmodified_PublishType;
			[DataMember]
			public string PublishContainer { get; set; }
			private string _unmodified_PublishContainer;
			[DataMember]
			public PublicationPackage ActivePublication { get; set; }
			private PublicationPackage _unmodified_ActivePublication;
			[DataMember]
			public PublicationPackageCollection Publications { get; set; }
			private PublicationPackageCollection _unmodified_Publications;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class PublicationPackageCollection 
			{

				public PublicationPackageCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "PublicationPackageCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(PublicationPackageCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static PublicationPackageCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(PublicationPackageCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (PublicationPackageCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<PublicationPackage> CollectionContent = new List<PublicationPackage>();
				private PublicationPackage[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public PublicationPackage[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(PublicationPackageCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class PublicationPackage 
			{

				public PublicationPackage()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "PublicationPackage";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(PublicationPackage));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static PublicationPackage DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(PublicationPackage));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (PublicationPackage) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(PublicationPackage sourceObject)
				{
					PackageName = sourceObject.PackageName;
					PublicationTime = sourceObject.PublicationTime;
				}
				



			[DataMember]
			public string PackageName { get; set; }
			private string _unmodified_PackageName;
			[DataMember]
			public DateTime PublicationTime { get; set; }
			private DateTime _unmodified_PublicationTime;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBRLoginRoot 
			{

				public TBRLoginRoot()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBRLoginRoot";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBRLoginRoot));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBRLoginRoot DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBRLoginRoot));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBRLoginRoot) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(TBRLoginRoot sourceObject)
				{
					DomainName = sourceObject.DomainName;
					Account = sourceObject.Account;
				}
				



			[DataMember]
			public string DomainName { get; set; }
			private string _unmodified_DomainName;
			[DataMember]
			public TBAccount Account { get; set; }
			private TBAccount _unmodified_Account;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBRAccountRoot 
			{

				public TBRAccountRoot()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBRAccountRoot";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBRAccountRoot));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBRAccountRoot DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBRAccountRoot));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBRAccountRoot) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(TBRAccountRoot sourceObject)
				{
					Account = sourceObject.Account;
				}
				



			[DataMember]
			public TBAccount Account { get; set; }
			private TBAccount _unmodified_Account;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBRGroupRoot 
			{

				public TBRGroupRoot()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBRGroupRoot";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBRGroupRoot));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBRGroupRoot DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBRGroupRoot));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBRGroupRoot) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(TBRGroupRoot sourceObject)
				{
					Group = sourceObject.Group;
				}
				



			[DataMember]
			public TBCollaboratingGroup Group { get; set; }
			private TBCollaboratingGroup _unmodified_Group;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBRLoginGroupRoot 
			{

				public TBRLoginGroupRoot()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBRLoginGroupRoot";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBRLoginGroupRoot));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBRLoginGroupRoot DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBRLoginGroupRoot));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBRLoginGroupRoot) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(TBRLoginGroupRoot sourceObject)
				{
					Role = sourceObject.Role;
					GroupID = sourceObject.GroupID;
				}
				



			[DataMember]
			public string Role { get; set; }
			private string _unmodified_Role;
			[DataMember]
			public string GroupID { get; set; }
			private string _unmodified_GroupID;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBREmailRoot 
			{

				public TBREmailRoot()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBREmailRoot";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBREmailRoot));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBREmailRoot DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBREmailRoot));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBREmailRoot) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(TBREmailRoot sourceObject)
				{
					Account = sourceObject.Account;
				}
				



			[DataMember]
			public TBAccount Account { get; set; }
			private TBAccount _unmodified_Account;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBAccount 
			{

				public TBAccount()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBAccount";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBAccount));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBAccount DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBAccount));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBAccount) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(TBAccount sourceObject)
				{
					Emails = sourceObject.Emails;
					Logins = sourceObject.Logins;
					GroupRoleCollection = sourceObject.GroupRoleCollection;
				}
				



			[DataMember]
			public TBEmailCollection Emails { get; set; }
			private TBEmailCollection _unmodified_Emails;
			[DataMember]
			public TBLoginInfoCollection Logins { get; set; }
			private TBLoginInfoCollection _unmodified_Logins;
			[DataMember]
			public TBAccountCollaborationGroupCollection GroupRoleCollection { get; set; }
			private TBAccountCollaborationGroupCollection _unmodified_GroupRoleCollection;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBAccountCollaborationGroup 
			{

				public TBAccountCollaborationGroup()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBAccountCollaborationGroup";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBAccountCollaborationGroup));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBAccountCollaborationGroup DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBAccountCollaborationGroup));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBAccountCollaborationGroup) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(TBAccountCollaborationGroup sourceObject)
				{
					GroupID = sourceObject.GroupID;
					GroupRole = sourceObject.GroupRole;
					RoleStatus = sourceObject.RoleStatus;
				}
				



			[DataMember]
			public string GroupID { get; set; }
			private string _unmodified_GroupID;
			[DataMember]
			public string GroupRole { get; set; }
			private string _unmodified_GroupRole;
			[DataMember]
			public string RoleStatus { get; set; }
			private string _unmodified_RoleStatus;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBAccountCollaborationGroupCollection 
			{

				public TBAccountCollaborationGroupCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBAccountCollaborationGroupCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBAccountCollaborationGroupCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBAccountCollaborationGroupCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBAccountCollaborationGroupCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBAccountCollaborationGroupCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<TBAccountCollaborationGroup> CollectionContent = new List<TBAccountCollaborationGroup>();
				private TBAccountCollaborationGroup[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public TBAccountCollaborationGroup[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(TBAccountCollaborationGroupCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBLoginInfo 
			{

				public TBLoginInfo()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBLoginInfo";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBLoginInfo));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBLoginInfo DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBLoginInfo));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBLoginInfo) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(TBLoginInfo sourceObject)
				{
					OpenIDUrl = sourceObject.OpenIDUrl;
				}
				



			[DataMember]
			public string OpenIDUrl { get; set; }
			private string _unmodified_OpenIDUrl;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBLoginInfoCollection 
			{

				public TBLoginInfoCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBLoginInfoCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBLoginInfoCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBLoginInfoCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBLoginInfoCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBLoginInfoCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<TBLoginInfo> CollectionContent = new List<TBLoginInfo>();
				private TBLoginInfo[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public TBLoginInfo[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(TBLoginInfoCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBEmail 
			{

				public TBEmail()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBEmail";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBEmail));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBEmail DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBEmail));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBEmail) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(TBEmail sourceObject)
				{
					EmailAddress = sourceObject.EmailAddress;
					ValidatedAt = sourceObject.ValidatedAt;
				}
				



			[DataMember]
			public string EmailAddress { get; set; }
			private string _unmodified_EmailAddress;
			[DataMember]
			public DateTime ValidatedAt { get; set; }
			private DateTime _unmodified_ValidatedAt;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBEmailCollection 
			{

				public TBEmailCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBEmailCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBEmailCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBEmailCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBEmailCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBEmailCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<TBEmail> CollectionContent = new List<TBEmail>();
				private TBEmail[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public TBEmail[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(TBEmailCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBCollaboratorRole 
			{

				public TBCollaboratorRole()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBCollaboratorRole";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBCollaboratorRole));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBCollaboratorRole DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBCollaboratorRole));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBCollaboratorRole) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(TBCollaboratorRole sourceObject)
				{
					Email = sourceObject.Email;
					Role = sourceObject.Role;
					RoleStatus = sourceObject.RoleStatus;
				}
				



			[DataMember]
			public TBEmail Email { get; set; }
			private TBEmail _unmodified_Email;
			[DataMember]
			public string Role { get; set; }
			private string _unmodified_Role;
			[DataMember]
			public string RoleStatus { get; set; }
			private string _unmodified_RoleStatus;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBCollaboratorRoleCollection 
			{

				public TBCollaboratorRoleCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBCollaboratorRoleCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBCollaboratorRoleCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBCollaboratorRoleCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBCollaboratorRoleCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBCollaboratorRoleCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<TBCollaboratorRole> CollectionContent = new List<TBCollaboratorRole>();
				private TBCollaboratorRole[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public TBCollaboratorRole[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(TBCollaboratorRoleCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBCollaboratingGroup 
			{

				public TBCollaboratingGroup()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBCollaboratingGroup";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBCollaboratingGroup));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBCollaboratingGroup DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBCollaboratingGroup));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBCollaboratingGroup) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(TBCollaboratingGroup sourceObject)
				{
					Title = sourceObject.Title;
					Roles = sourceObject.Roles;
				}
				



			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public TBCollaboratorRoleCollection Roles { get; set; }
			private TBCollaboratorRoleCollection _unmodified_Roles;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBEmailValidation 
			{

				public TBEmailValidation()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBEmailValidation";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBEmailValidation));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBEmailValidation DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBEmailValidation));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBEmailValidation) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(TBEmailValidation sourceObject)
				{
					Email = sourceObject.Email;
					AccountID = sourceObject.AccountID;
					ValidUntil = sourceObject.ValidUntil;
					GroupJoinConfirmation = sourceObject.GroupJoinConfirmation;
					DeviceJoinConfirmation = sourceObject.DeviceJoinConfirmation;
					InformationInputConfirmation = sourceObject.InformationInputConfirmation;
					InformationOutputConfirmation = sourceObject.InformationOutputConfirmation;
					MergeAccountsConfirmation = sourceObject.MergeAccountsConfirmation;
					RedirectUrlAfterValidation = sourceObject.RedirectUrlAfterValidation;
				}
				



			[DataMember]
			public string Email { get; set; }
			private string _unmodified_Email;
			[DataMember]
			public string AccountID { get; set; }
			private string _unmodified_AccountID;
			[DataMember]
			public DateTime ValidUntil { get; set; }
			private DateTime _unmodified_ValidUntil;
			[DataMember]
			public TBGroupJoinConfirmation GroupJoinConfirmation { get; set; }
			private TBGroupJoinConfirmation _unmodified_GroupJoinConfirmation;
			[DataMember]
			public TBDeviceJoinConfirmation DeviceJoinConfirmation { get; set; }
			private TBDeviceJoinConfirmation _unmodified_DeviceJoinConfirmation;
			[DataMember]
			public TBInformationInputConfirmation InformationInputConfirmation { get; set; }
			private TBInformationInputConfirmation _unmodified_InformationInputConfirmation;
			[DataMember]
			public TBInformationOutputConfirmation InformationOutputConfirmation { get; set; }
			private TBInformationOutputConfirmation _unmodified_InformationOutputConfirmation;
			[DataMember]
			public TBMergeAccountConfirmation MergeAccountsConfirmation { get; set; }
			private TBMergeAccountConfirmation _unmodified_MergeAccountsConfirmation;
			[DataMember]
			public string RedirectUrlAfterValidation { get; set; }
			private string _unmodified_RedirectUrlAfterValidation;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBMergeAccountConfirmation 
			{

				public TBMergeAccountConfirmation()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBMergeAccountConfirmation";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBMergeAccountConfirmation));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBMergeAccountConfirmation DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBMergeAccountConfirmation));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBMergeAccountConfirmation) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(TBMergeAccountConfirmation sourceObject)
				{
					AccountToBeMergedID = sourceObject.AccountToBeMergedID;
					AccountToMergeToID = sourceObject.AccountToMergeToID;
				}
				



			[DataMember]
			public string AccountToBeMergedID { get; set; }
			private string _unmodified_AccountToBeMergedID;
			[DataMember]
			public string AccountToMergeToID { get; set; }
			private string _unmodified_AccountToMergeToID;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBGroupJoinConfirmation 
			{

				public TBGroupJoinConfirmation()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBGroupJoinConfirmation";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBGroupJoinConfirmation));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBGroupJoinConfirmation DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBGroupJoinConfirmation));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBGroupJoinConfirmation) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(TBGroupJoinConfirmation sourceObject)
				{
					GroupID = sourceObject.GroupID;
					InvitationMode = sourceObject.InvitationMode;
				}
				



			[DataMember]
			public string GroupID { get; set; }
			private string _unmodified_GroupID;
			[DataMember]
			public string InvitationMode { get; set; }
			private string _unmodified_InvitationMode;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBDeviceJoinConfirmation 
			{

				public TBDeviceJoinConfirmation()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBDeviceJoinConfirmation";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBDeviceJoinConfirmation));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBDeviceJoinConfirmation DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBDeviceJoinConfirmation));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBDeviceJoinConfirmation) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(TBDeviceJoinConfirmation sourceObject)
				{
					GroupID = sourceObject.GroupID;
					AccountID = sourceObject.AccountID;
					DeviceMembershipID = sourceObject.DeviceMembershipID;
				}
				



			[DataMember]
			public string GroupID { get; set; }
			private string _unmodified_GroupID;
			[DataMember]
			public string AccountID { get; set; }
			private string _unmodified_AccountID;
			[DataMember]
			public string DeviceMembershipID { get; set; }
			private string _unmodified_DeviceMembershipID;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBInformationInputConfirmation 
			{

				public TBInformationInputConfirmation()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBInformationInputConfirmation";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBInformationInputConfirmation));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBInformationInputConfirmation DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBInformationInputConfirmation));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBInformationInputConfirmation) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(TBInformationInputConfirmation sourceObject)
				{
					GroupID = sourceObject.GroupID;
					AccountID = sourceObject.AccountID;
					InformationInputID = sourceObject.InformationInputID;
				}
				



			[DataMember]
			public string GroupID { get; set; }
			private string _unmodified_GroupID;
			[DataMember]
			public string AccountID { get; set; }
			private string _unmodified_AccountID;
			[DataMember]
			public string InformationInputID { get; set; }
			private string _unmodified_InformationInputID;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBInformationOutputConfirmation 
			{

				public TBInformationOutputConfirmation()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBInformationOutputConfirmation";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBInformationOutputConfirmation));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBInformationOutputConfirmation DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBInformationOutputConfirmation));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBInformationOutputConfirmation) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(TBInformationOutputConfirmation sourceObject)
				{
					GroupID = sourceObject.GroupID;
					AccountID = sourceObject.AccountID;
					InformationOutputID = sourceObject.InformationOutputID;
				}
				



			[DataMember]
			public string GroupID { get; set; }
			private string _unmodified_GroupID;
			[DataMember]
			public string AccountID { get; set; }
			private string _unmodified_AccountID;
			[DataMember]
			public string InformationOutputID { get; set; }
			private string _unmodified_InformationOutputID;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBRegisterContainer 
			{

				public TBRegisterContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBRegisterContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBRegisterContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBRegisterContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBRegisterContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBRegisterContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(TBRegisterContainer sourceObject)
				{
					Header = sourceObject.Header;
					ReturnUrl = sourceObject.ReturnUrl;
					LoginProviderCollection = sourceObject.LoginProviderCollection;
				}
				



			[DataMember]
			public ContainerHeader Header { get; set; }
			private ContainerHeader _unmodified_Header;
			[DataMember]
			public string ReturnUrl { get; set; }
			private string _unmodified_ReturnUrl;
			[DataMember]
			public LoginProviderCollection LoginProviderCollection { get; set; }
			private LoginProviderCollection _unmodified_LoginProviderCollection;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class LoginProvider 
			{

				public LoginProvider()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "LoginProvider";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(LoginProvider));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static LoginProvider DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(LoginProvider));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (LoginProvider) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(LoginProvider sourceObject)
				{
					ProviderName = sourceObject.ProviderName;
					ProviderIconClass = sourceObject.ProviderIconClass;
					ProviderType = sourceObject.ProviderType;
					ProviderUrl = sourceObject.ProviderUrl;
					ReturnUrl = sourceObject.ReturnUrl;
				}
				



			[DataMember]
			public string ProviderName { get; set; }
			private string _unmodified_ProviderName;
			[DataMember]
			public string ProviderIconClass { get; set; }
			private string _unmodified_ProviderIconClass;
			[DataMember]
			public string ProviderType { get; set; }
			private string _unmodified_ProviderType;
			[DataMember]
			public string ProviderUrl { get; set; }
			private string _unmodified_ProviderUrl;
			[DataMember]
			public string ReturnUrl { get; set; }
			private string _unmodified_ReturnUrl;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class LoginProviderCollection 
			{

				public LoginProviderCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "LoginProviderCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(LoginProviderCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static LoginProviderCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(LoginProviderCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (LoginProviderCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<LoginProvider> CollectionContent = new List<LoginProvider>();
				private LoginProvider[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public LoginProvider[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(LoginProviderCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class ContactOipContainer 
			{

				public ContactOipContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "ContactOipContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ContactOipContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static ContactOipContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ContactOipContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (ContactOipContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(ContactOipContainer sourceObject)
				{
					OIPModeratorGroupID = sourceObject.OIPModeratorGroupID;
				}
				



			[DataMember]
			public string OIPModeratorGroupID { get; set; }
			private string _unmodified_OIPModeratorGroupID;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TBPRegisterEmail 
			{

				public TBPRegisterEmail()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TBPRegisterEmail";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBPRegisterEmail));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TBPRegisterEmail DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TBPRegisterEmail));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TBPRegisterEmail) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(TBPRegisterEmail sourceObject)
				{
					EmailAddress = sourceObject.EmailAddress;
				}
				



			[DataMember]
			public string EmailAddress { get; set; }
			private string _unmodified_EmailAddress;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class JavaScriptContainer 
			{

				public JavaScriptContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "JavaScriptContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(JavaScriptContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static JavaScriptContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(JavaScriptContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (JavaScriptContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(JavaScriptContainer sourceObject)
				{
					HtmlContent = sourceObject.HtmlContent;
				}
				



			[DataMember]
			public string HtmlContent { get; set; }
			private string _unmodified_HtmlContent;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class JavascriptContainer 
			{

				public JavascriptContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "JavascriptContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(JavascriptContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static JavascriptContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(JavascriptContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (JavascriptContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(JavascriptContainer sourceObject)
				{
					HtmlContent = sourceObject.HtmlContent;
				}
				



			[DataMember]
			public string HtmlContent { get; set; }
			private string _unmodified_HtmlContent;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class FooterContainer 
			{

				public FooterContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "FooterContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(FooterContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static FooterContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(FooterContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (FooterContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(FooterContainer sourceObject)
				{
					HtmlContent = sourceObject.HtmlContent;
				}
				



			[DataMember]
			public string HtmlContent { get; set; }
			private string _unmodified_HtmlContent;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class NavigationContainer 
			{

				public NavigationContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "NavigationContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(NavigationContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static NavigationContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(NavigationContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (NavigationContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(NavigationContainer sourceObject)
				{
					Dummy = sourceObject.Dummy;
				}
				



			[DataMember]
			public string Dummy { get; set; }
			private string _unmodified_Dummy;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class AccountSummary 
			{

				public AccountSummary()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "AccountSummary";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AccountSummary));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static AccountSummary DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AccountSummary));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (AccountSummary) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(AccountSummary sourceObject)
				{
					Introduction = sourceObject.Introduction;
					ActivitySummary = sourceObject.ActivitySummary;
					GroupSummary = sourceObject.GroupSummary;
				}
				



			[DataMember]
			public Introduction Introduction { get; set; }
			private Introduction _unmodified_Introduction;
			[DataMember]
			public ActivitySummaryContainer ActivitySummary { get; set; }
			private ActivitySummaryContainer _unmodified_ActivitySummary;
			[DataMember]
			public GroupSummaryContainer GroupSummary { get; set; }
			private GroupSummaryContainer _unmodified_GroupSummary;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class AccountContainer 
			{

				public AccountContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "AccountContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AccountContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static AccountContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AccountContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (AccountContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(AccountContainer sourceObject)
				{
					Header = sourceObject.Header;
					AccountIndex = sourceObject.AccountIndex;
					AccountModule = sourceObject.AccountModule;
					AccountSummary = sourceObject.AccountSummary;
				}
				



			[DataMember]
			public ContainerHeader Header { get; set; }
			private ContainerHeader _unmodified_Header;
			[DataMember]
			public AccountIndex AccountIndex { get; set; }
			private AccountIndex _unmodified_AccountIndex;
			[DataMember]
			public AccountModule AccountModule { get; set; }
			private AccountModule _unmodified_AccountModule;
			[DataMember]
			public AccountSummary AccountSummary { get; set; }
			private AccountSummary _unmodified_AccountSummary;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class AccountIndex 
			{

				public AccountIndex()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "AccountIndex";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AccountIndex));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static AccountIndex DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AccountIndex));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (AccountIndex) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(AccountIndex sourceObject)
				{
					Icon = sourceObject.Icon;
					Title = sourceObject.Title;
					Introduction = sourceObject.Introduction;
					Summary = sourceObject.Summary;
				}
				



			[DataMember]
			public Image Icon { get; set; }
			private Image _unmodified_Icon;
			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public string Introduction { get; set; }
			private string _unmodified_Introduction;
			[DataMember]
			public string Summary { get; set; }
			private string _unmodified_Summary;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class AccountModule 
			{

				public AccountModule()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "AccountModule";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AccountModule));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static AccountModule DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AccountModule));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (AccountModule) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(AccountModule sourceObject)
				{
					Profile = sourceObject.Profile;
					Security = sourceObject.Security;
					Roles = sourceObject.Roles;
					LocationCollection = sourceObject.LocationCollection;
				}
				



			[DataMember]
			public AccountProfile Profile { get; set; }
			private AccountProfile _unmodified_Profile;
			[DataMember]
			public AccountSecurity Security { get; set; }
			private AccountSecurity _unmodified_Security;
			[DataMember]
			public AccountRoles Roles { get; set; }
			private AccountRoles _unmodified_Roles;
			[DataMember]
			public AddressAndLocationCollection LocationCollection { get; set; }
			private AddressAndLocationCollection _unmodified_LocationCollection;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class ImageGroupContainer 
			{

				public ImageGroupContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "ImageGroupContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ImageGroupContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static ImageGroupContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ImageGroupContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (ImageGroupContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(ImageGroupContainer sourceObject)
				{
					ImageGroups = sourceObject.ImageGroups;
				}
				



			[DataMember]
			public ImageGroupCollection ImageGroups { get; set; }
			private ImageGroupCollection _unmodified_ImageGroups;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class LocationContainer 
			{

				public LocationContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "LocationContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(LocationContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static LocationContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(LocationContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (LocationContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(LocationContainer sourceObject)
				{
					Locations = sourceObject.Locations;
				}
				



			[DataMember]
			public AddressAndLocationCollection Locations { get; set; }
			private AddressAndLocationCollection _unmodified_Locations;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class AddressAndLocationCollection 
			{

				public AddressAndLocationCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "AddressAndLocationCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AddressAndLocationCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static AddressAndLocationCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AddressAndLocationCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (AddressAndLocationCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<AddressAndLocation> CollectionContent = new List<AddressAndLocation>();
				private AddressAndLocation[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public AddressAndLocation[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(AddressAndLocationCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class AddressAndLocation 
			{

				public AddressAndLocation()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "AddressAndLocation";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AddressAndLocation));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static AddressAndLocation DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AddressAndLocation));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (AddressAndLocation) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(AddressAndLocation sourceObject)
				{
					ReferenceToInformation = sourceObject.ReferenceToInformation;
					Address = sourceObject.Address;
					Location = sourceObject.Location;
				}
				



			[DataMember]
			public ReferenceToInformation ReferenceToInformation { get; set; }
			private ReferenceToInformation _unmodified_ReferenceToInformation;
			[DataMember]
			public StreetAddress Address { get; set; }
			private StreetAddress _unmodified_Address;
			[DataMember]
			public Location Location { get; set; }
			private Location _unmodified_Location;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class StreetAddress 
			{

				public StreetAddress()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "StreetAddress";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(StreetAddress));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static StreetAddress DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(StreetAddress));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (StreetAddress) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(StreetAddress sourceObject)
				{
					Street = sourceObject.Street;
					ZipCode = sourceObject.ZipCode;
					Town = sourceObject.Town;
					Country = sourceObject.Country;
				}
				



			[DataMember]
			public string Street { get; set; }
			private string _unmodified_Street;
			[DataMember]
			public string ZipCode { get; set; }
			private string _unmodified_ZipCode;
			[DataMember]
			public string Town { get; set; }
			private string _unmodified_Town;
			[DataMember]
			public string Country { get; set; }
			private string _unmodified_Country;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class AccountContent 
			{

				public AccountContent()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "AccountContent";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AccountContent));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static AccountContent DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AccountContent));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (AccountContent) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(AccountContent sourceObject)
				{
					Dummy = sourceObject.Dummy;
				}
				



			[DataMember]
			public string Dummy { get; set; }
			private string _unmodified_Dummy;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class AccountProfile 
			{

				public AccountProfile()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "AccountProfile";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AccountProfile));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static AccountProfile DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AccountProfile));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (AccountProfile) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(AccountProfile sourceObject)
				{
					ProfileImage = sourceObject.ProfileImage;
					FirstName = sourceObject.FirstName;
					LastName = sourceObject.LastName;
					Address = sourceObject.Address;
					IsSimplifiedAccount = sourceObject.IsSimplifiedAccount;
					SimplifiedAccountEmail = sourceObject.SimplifiedAccountEmail;
					SimplifiedAccountGroupID = sourceObject.SimplifiedAccountGroupID;
				}
				



			[DataMember]
			public Image ProfileImage { get; set; }
			private Image _unmodified_ProfileImage;
			[DataMember]
			public string FirstName { get; set; }
			private string _unmodified_FirstName;
			[DataMember]
			public string LastName { get; set; }
			private string _unmodified_LastName;
			[DataMember]
			public StreetAddress Address { get; set; }
			private StreetAddress _unmodified_Address;
			[DataMember]
			public bool IsSimplifiedAccount { get; set; }
			private bool _unmodified_IsSimplifiedAccount;
			[DataMember]
			public string SimplifiedAccountEmail { get; set; }
			private string _unmodified_SimplifiedAccountEmail;
			[DataMember]
			public string SimplifiedAccountGroupID { get; set; }
			private string _unmodified_SimplifiedAccountGroupID;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class AccountSecurity 
			{

				public AccountSecurity()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "AccountSecurity";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AccountSecurity));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static AccountSecurity DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AccountSecurity));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (AccountSecurity) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(AccountSecurity sourceObject)
				{
					LoginInfoCollection = sourceObject.LoginInfoCollection;
					EmailCollection = sourceObject.EmailCollection;
				}
				



			[DataMember]
			public TBLoginInfoCollection LoginInfoCollection { get; set; }
			private TBLoginInfoCollection _unmodified_LoginInfoCollection;
			[DataMember]
			public TBEmailCollection EmailCollection { get; set; }
			private TBEmailCollection _unmodified_EmailCollection;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class AccountRoles 
			{

				public AccountRoles()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "AccountRoles";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AccountRoles));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static AccountRoles DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AccountRoles));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (AccountRoles) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(AccountRoles sourceObject)
				{
					ModeratorInGroups = sourceObject.ModeratorInGroups;
					MemberInGroups = sourceObject.MemberInGroups;
					OrganizationsImPartOf = sourceObject.OrganizationsImPartOf;
				}
				



			[DataMember]
			public ReferenceCollection ModeratorInGroups { get; set; }
			private ReferenceCollection _unmodified_ModeratorInGroups;
			[DataMember]
			public ReferenceCollection MemberInGroups { get; set; }
			private ReferenceCollection _unmodified_MemberInGroups;
			[DataMember]
			public string OrganizationsImPartOf { get; set; }
			private string _unmodified_OrganizationsImPartOf;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class PersonalInfoVisibility 
			{

				public PersonalInfoVisibility()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "PersonalInfoVisibility";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(PersonalInfoVisibility));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static PersonalInfoVisibility DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(PersonalInfoVisibility));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (PersonalInfoVisibility) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(PersonalInfoVisibility sourceObject)
				{
					NoOne_Network_All = sourceObject.NoOne_Network_All;
				}
				



			[DataMember]
			public string NoOne_Network_All { get; set; }
			private string _unmodified_NoOne_Network_All;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class GroupedInformationCollection 
			{

				public GroupedInformationCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "GroupedInformationCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(GroupedInformationCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static GroupedInformationCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(GroupedInformationCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (GroupedInformationCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<GroupedInformation> CollectionContent = new List<GroupedInformation>();
				private GroupedInformation[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public GroupedInformation[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(GroupedInformationCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class GroupedInformation 
			{

				public GroupedInformation()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "GroupedInformation";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(GroupedInformation));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static GroupedInformation DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(GroupedInformation));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (GroupedInformation) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(GroupedInformation sourceObject)
				{
					GroupName = sourceObject.GroupName;
					ReferenceCollection = sourceObject.ReferenceCollection;
				}
				



			[DataMember]
			public string GroupName { get; set; }
			private string _unmodified_GroupName;
			[DataMember]
			public ReferenceCollection ReferenceCollection { get; set; }
			private ReferenceCollection _unmodified_ReferenceCollection;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class ReferenceToInformation 
			{

				public ReferenceToInformation()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "ReferenceToInformation";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ReferenceToInformation));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static ReferenceToInformation DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ReferenceToInformation));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (ReferenceToInformation) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(ReferenceToInformation sourceObject)
				{
					Title = sourceObject.Title;
					URL = sourceObject.URL;
				}
				



			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public string URL { get; set; }
			private string _unmodified_URL;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class ReferenceCollection 
			{

				public ReferenceCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "ReferenceCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ReferenceCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static ReferenceCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ReferenceCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (ReferenceCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<ReferenceToInformation> CollectionContent = new List<ReferenceToInformation>();
				private ReferenceToInformation[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public ReferenceToInformation[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(ReferenceCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class BlogContainer 
			{

				public BlogContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "BlogContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(BlogContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static BlogContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(BlogContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (BlogContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(BlogContainer sourceObject)
				{
					Header = sourceObject.Header;
					FeaturedBlog = sourceObject.FeaturedBlog;
					RecentBlogSummary = sourceObject.RecentBlogSummary;
					BlogIndexGroup = sourceObject.BlogIndexGroup;
				}
				



			[DataMember]
			public ContainerHeader Header { get; set; }
			private ContainerHeader _unmodified_Header;
			[DataMember]
			public Blog FeaturedBlog { get; set; }
			private Blog _unmodified_FeaturedBlog;
			[DataMember]
			public RecentBlogSummary RecentBlogSummary { get; set; }
			private RecentBlogSummary _unmodified_RecentBlogSummary;
			[DataMember]
			public BlogIndexGroup BlogIndexGroup { get; set; }
			private BlogIndexGroup _unmodified_BlogIndexGroup;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class RecentBlogSummary 
			{

				public RecentBlogSummary()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "RecentBlogSummary";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(RecentBlogSummary));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static RecentBlogSummary DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(RecentBlogSummary));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (RecentBlogSummary) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(RecentBlogSummary sourceObject)
				{
					Introduction = sourceObject.Introduction;
					RecentBlogCollection = sourceObject.RecentBlogCollection;
				}
				



			[DataMember]
			public Introduction Introduction { get; set; }
			private Introduction _unmodified_Introduction;
			[DataMember]
			public BlogCollection RecentBlogCollection { get; set; }
			private BlogCollection _unmodified_RecentBlogCollection;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class NodeSummaryContainer 
			{

				public NodeSummaryContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "NodeSummaryContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(NodeSummaryContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static NodeSummaryContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(NodeSummaryContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (NodeSummaryContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(NodeSummaryContainer sourceObject)
				{
					Nodes = sourceObject.Nodes;
					NodeSourceBlogs = sourceObject.NodeSourceBlogs;
					NodeSourceActivities = sourceObject.NodeSourceActivities;
					NodeSourceTextContent = sourceObject.NodeSourceTextContent;
					NodeSourceLinkToContent = sourceObject.NodeSourceLinkToContent;
					NodeSourceEmbeddedContent = sourceObject.NodeSourceEmbeddedContent;
					NodeSourceImages = sourceObject.NodeSourceImages;
					NodeSourceBinaryFiles = sourceObject.NodeSourceBinaryFiles;
					NodeSourceCategories = sourceObject.NodeSourceCategories;
				}
				



			[DataMember]
			public RenderedNodeCollection Nodes { get; set; }
			private RenderedNodeCollection _unmodified_Nodes;
			[DataMember]
			public BlogCollection NodeSourceBlogs { get; set; }
			private BlogCollection _unmodified_NodeSourceBlogs;
			[DataMember]
			public ActivityCollection NodeSourceActivities { get; set; }
			private ActivityCollection _unmodified_NodeSourceActivities;
			[DataMember]
			public TextContentCollection NodeSourceTextContent { get; set; }
			private TextContentCollection _unmodified_NodeSourceTextContent;
			[DataMember]
			public LinkToContentCollection NodeSourceLinkToContent { get; set; }
			private LinkToContentCollection _unmodified_NodeSourceLinkToContent;
			[DataMember]
			public EmbeddedContentCollection NodeSourceEmbeddedContent { get; set; }
			private EmbeddedContentCollection _unmodified_NodeSourceEmbeddedContent;
			[DataMember]
			public ImageCollection NodeSourceImages { get; set; }
			private ImageCollection _unmodified_NodeSourceImages;
			[DataMember]
			public BinaryFileCollection NodeSourceBinaryFiles { get; set; }
			private BinaryFileCollection _unmodified_NodeSourceBinaryFiles;
			[DataMember]
			public CategoryCollection NodeSourceCategories { get; set; }
			private CategoryCollection _unmodified_NodeSourceCategories;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class RenderedNodeCollection 
			{

				public RenderedNodeCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "RenderedNodeCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(RenderedNodeCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static RenderedNodeCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(RenderedNodeCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (RenderedNodeCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<RenderedNode> CollectionContent = new List<RenderedNode>();
				private RenderedNode[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public RenderedNode[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(RenderedNodeCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class RenderedNode 
			{

				public RenderedNode()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "RenderedNode";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(RenderedNode));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static RenderedNode DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(RenderedNode));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (RenderedNode) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(RenderedNode sourceObject)
				{
					OriginalContentID = sourceObject.OriginalContentID;
					TechnicalSource = sourceObject.TechnicalSource;
					ImageBaseUrl = sourceObject.ImageBaseUrl;
					ImageExt = sourceObject.ImageExt;
					Title = sourceObject.Title;
					ActualContentUrl = sourceObject.ActualContentUrl;
					Excerpt = sourceObject.Excerpt;
					TimestampText = sourceObject.TimestampText;
					MainSortableText = sourceObject.MainSortableText;
					IsCategoryFilteringNode = sourceObject.IsCategoryFilteringNode;
					CategoryFilters = sourceObject.CategoryFilters;
					CategoryNames = sourceObject.CategoryNames;
					Categories = sourceObject.Categories;
					CategoryIDList = sourceObject.CategoryIDList;
					Authors = sourceObject.Authors;
					Locations = sourceObject.Locations;
				}
				



			[DataMember]
			public string OriginalContentID { get; set; }
			private string _unmodified_OriginalContentID;
			[DataMember]
			public string TechnicalSource { get; set; }
			private string _unmodified_TechnicalSource;
			[DataMember]
			public string ImageBaseUrl { get; set; }
			private string _unmodified_ImageBaseUrl;
			[DataMember]
			public string ImageExt { get; set; }
			private string _unmodified_ImageExt;
			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public string ActualContentUrl { get; set; }
			private string _unmodified_ActualContentUrl;
			[DataMember]
			public string Excerpt { get; set; }
			private string _unmodified_Excerpt;
			[DataMember]
			public string TimestampText { get; set; }
			private string _unmodified_TimestampText;
			[DataMember]
			public string MainSortableText { get; set; }
			private string _unmodified_MainSortableText;
			[DataMember]
			public bool IsCategoryFilteringNode { get; set; }
			private bool _unmodified_IsCategoryFilteringNode;
			[DataMember]
			public ShortTextCollection CategoryFilters { get; set; }
			private ShortTextCollection _unmodified_CategoryFilters;
			[DataMember]
			public ShortTextCollection CategoryNames { get; set; }
			private ShortTextCollection _unmodified_CategoryNames;
			[DataMember]
			public ShortTextCollection Categories { get; set; }
			private ShortTextCollection _unmodified_Categories;
			[DataMember]
			public string CategoryIDList { get; set; }
			private string _unmodified_CategoryIDList;
			[DataMember]
			public ShortTextCollection Authors { get; set; }
			private ShortTextCollection _unmodified_Authors;
			[DataMember]
			public ShortTextCollection Locations { get; set; }
			private ShortTextCollection _unmodified_Locations;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class ShortTextCollection 
			{

				public ShortTextCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "ShortTextCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ShortTextCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static ShortTextCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ShortTextCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (ShortTextCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<ShortTextObject> CollectionContent = new List<ShortTextObject>();
				private ShortTextObject[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public ShortTextObject[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(ShortTextCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class ShortTextObject 
			{

				public ShortTextObject()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "ShortTextObject";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ShortTextObject));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static ShortTextObject DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ShortTextObject));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (ShortTextObject) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(ShortTextObject sourceObject)
				{
					Content = sourceObject.Content;
				}
				



			[DataMember]
			public string Content { get; set; }
			private string _unmodified_Content;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class LongTextCollection 
			{

				public LongTextCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "LongTextCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(LongTextCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static LongTextCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(LongTextCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (LongTextCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<LongTextObject> CollectionContent = new List<LongTextObject>();
				private LongTextObject[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public LongTextObject[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(LongTextCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class LongTextObject 
			{

				public LongTextObject()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "LongTextObject";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(LongTextObject));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static LongTextObject DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(LongTextObject));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (LongTextObject) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(LongTextObject sourceObject)
				{
					Content = sourceObject.Content;
				}
				



			[DataMember]
			public string Content { get; set; }
			private string _unmodified_Content;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class MapContainer 
			{

				public MapContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "MapContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(MapContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static MapContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(MapContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (MapContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(MapContainer sourceObject)
				{
					Header = sourceObject.Header;
					MapFeatured = sourceObject.MapFeatured;
					MapCollection = sourceObject.MapCollection;
					MapResultCollection = sourceObject.MapResultCollection;
					MapIndexCollection = sourceObject.MapIndexCollection;
					MarkerSourceLocations = sourceObject.MarkerSourceLocations;
					MarkerSourceBlogs = sourceObject.MarkerSourceBlogs;
					MarkerSourceActivities = sourceObject.MarkerSourceActivities;
					MapMarkers = sourceObject.MapMarkers;
				}
				



			[DataMember]
			public ContainerHeader Header { get; set; }
			private ContainerHeader _unmodified_Header;
			[DataMember]
			public Map MapFeatured { get; set; }
			private Map _unmodified_MapFeatured;
			[DataMember]
			public MapCollection MapCollection { get; set; }
			private MapCollection _unmodified_MapCollection;
			[DataMember]
			public MapResultCollection MapResultCollection { get; set; }
			private MapResultCollection _unmodified_MapResultCollection;
			[DataMember]
			public MapIndexCollection MapIndexCollection { get; set; }
			private MapIndexCollection _unmodified_MapIndexCollection;
			[DataMember]
			public AddressAndLocationCollection MarkerSourceLocations { get; set; }
			private AddressAndLocationCollection _unmodified_MarkerSourceLocations;
			[DataMember]
			public BlogCollection MarkerSourceBlogs { get; set; }
			private BlogCollection _unmodified_MarkerSourceBlogs;
			[DataMember]
			public ActivityCollection MarkerSourceActivities { get; set; }
			private ActivityCollection _unmodified_MarkerSourceActivities;
			[DataMember]
			public MapMarkerCollection MapMarkers { get; set; }
			private MapMarkerCollection _unmodified_MapMarkers;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class MapMarker 
			{

				public MapMarker()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "MapMarker";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(MapMarker));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static MapMarker DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(MapMarker));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (MapMarker) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(MapMarker sourceObject)
				{
					IconUrl = sourceObject.IconUrl;
					MarkerSource = sourceObject.MarkerSource;
					CategoryName = sourceObject.CategoryName;
					LocationText = sourceObject.LocationText;
					PopupTitle = sourceObject.PopupTitle;
					PopupContent = sourceObject.PopupContent;
					Location = sourceObject.Location;
				}
				



			[DataMember]
			public string IconUrl { get; set; }
			private string _unmodified_IconUrl;
			[DataMember]
			public string MarkerSource { get; set; }
			private string _unmodified_MarkerSource;
			[DataMember]
			public string CategoryName { get; set; }
			private string _unmodified_CategoryName;
			[DataMember]
			public string LocationText { get; set; }
			private string _unmodified_LocationText;
			[DataMember]
			public string PopupTitle { get; set; }
			private string _unmodified_PopupTitle;
			[DataMember]
			public string PopupContent { get; set; }
			private string _unmodified_PopupContent;
			[DataMember]
			public Location Location { get; set; }
			private Location _unmodified_Location;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class MapMarkerCollection 
			{

				public MapMarkerCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "MapMarkerCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(MapMarkerCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static MapMarkerCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(MapMarkerCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (MapMarkerCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<MapMarker> CollectionContent = new List<MapMarker>();
				private MapMarker[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public MapMarker[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(MapMarkerCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class CalendarContainer 
			{

				public CalendarContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "CalendarContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(CalendarContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static CalendarContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(CalendarContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (CalendarContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(CalendarContainer sourceObject)
				{
					CalendarContainerHeader = sourceObject.CalendarContainerHeader;
					CalendarFeatured = sourceObject.CalendarFeatured;
					CalendarCollection = sourceObject.CalendarCollection;
					CalendarIndexCollection = sourceObject.CalendarIndexCollection;
				}
				



			[DataMember]
			public ContainerHeader CalendarContainerHeader { get; set; }
			private ContainerHeader _unmodified_CalendarContainerHeader;
			[DataMember]
			public Calendar CalendarFeatured { get; set; }
			private Calendar _unmodified_CalendarFeatured;
			[DataMember]
			public CalendarCollection CalendarCollection { get; set; }
			private CalendarCollection _unmodified_CalendarCollection;
			[DataMember]
			public CalendarIndex CalendarIndexCollection { get; set; }
			private CalendarIndex _unmodified_CalendarIndexCollection;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class AboutContainer 
			{

				public AboutContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "AboutContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AboutContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static AboutContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AboutContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (AboutContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(AboutContainer sourceObject)
				{
					MainImage = sourceObject.MainImage;
					Header = sourceObject.Header;
					Excerpt = sourceObject.Excerpt;
					Body = sourceObject.Body;
					Published = sourceObject.Published;
					Author = sourceObject.Author;
					ImageGroup = sourceObject.ImageGroup;
				}
				



			[DataMember]
			public Image MainImage { get; set; }
			private Image _unmodified_MainImage;
			[DataMember]
			public ContainerHeader Header { get; set; }
			private ContainerHeader _unmodified_Header;
			[DataMember]
			public string Excerpt { get; set; }
			private string _unmodified_Excerpt;
			[DataMember]
			public string Body { get; set; }
			private string _unmodified_Body;
			[DataMember]
			public DateTime Published { get; set; }
			private DateTime _unmodified_Published;
			[DataMember]
			public string Author { get; set; }
			private string _unmodified_Author;
			[DataMember]
			public ImageGroup ImageGroup { get; set; }
			private ImageGroup _unmodified_ImageGroup;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class OBSAccountContainer 
			{

				public OBSAccountContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "OBSAccountContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(OBSAccountContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static OBSAccountContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(OBSAccountContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (OBSAccountContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(OBSAccountContainer sourceObject)
				{
					AccountContainerHeader = sourceObject.AccountContainerHeader;
					AccountFeatured = sourceObject.AccountFeatured;
					AccountCollection = sourceObject.AccountCollection;
					AccountIndexCollection = sourceObject.AccountIndexCollection;
				}
				



			[DataMember]
			public ContainerHeader AccountContainerHeader { get; set; }
			private ContainerHeader _unmodified_AccountContainerHeader;
			[DataMember]
			public Calendar AccountFeatured { get; set; }
			private Calendar _unmodified_AccountFeatured;
			[DataMember]
			public CalendarCollection AccountCollection { get; set; }
			private CalendarCollection _unmodified_AccountCollection;
			[DataMember]
			public CalendarIndex AccountIndexCollection { get; set; }
			private CalendarIndex _unmodified_AccountIndexCollection;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class ProjectContainer 
			{

				public ProjectContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "ProjectContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ProjectContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static ProjectContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ProjectContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (ProjectContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(ProjectContainer sourceObject)
				{
					ProjectContainerHeader = sourceObject.ProjectContainerHeader;
					ProjectFeatured = sourceObject.ProjectFeatured;
					ProjectCollection = sourceObject.ProjectCollection;
					ProjectIndexCollection = sourceObject.ProjectIndexCollection;
				}
				



			[DataMember]
			public ContainerHeader ProjectContainerHeader { get; set; }
			private ContainerHeader _unmodified_ProjectContainerHeader;
			[DataMember]
			public Calendar ProjectFeatured { get; set; }
			private Calendar _unmodified_ProjectFeatured;
			[DataMember]
			public CalendarCollection ProjectCollection { get; set; }
			private CalendarCollection _unmodified_ProjectCollection;
			[DataMember]
			public CalendarIndex ProjectIndexCollection { get; set; }
			private CalendarIndex _unmodified_ProjectIndexCollection;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class CourseContainer 
			{

				public CourseContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "CourseContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(CourseContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static CourseContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(CourseContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (CourseContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(CourseContainer sourceObject)
				{
					CourseContainerHeader = sourceObject.CourseContainerHeader;
					CourseFeatured = sourceObject.CourseFeatured;
					CourseCollection = sourceObject.CourseCollection;
					CourseIndexCollection = sourceObject.CourseIndexCollection;
				}
				



			[DataMember]
			public ContainerHeader CourseContainerHeader { get; set; }
			private ContainerHeader _unmodified_CourseContainerHeader;
			[DataMember]
			public Calendar CourseFeatured { get; set; }
			private Calendar _unmodified_CourseFeatured;
			[DataMember]
			public CalendarCollection CourseCollection { get; set; }
			private CalendarCollection _unmodified_CourseCollection;
			[DataMember]
			public CalendarIndex CourseIndexCollection { get; set; }
			private CalendarIndex _unmodified_CourseIndexCollection;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class ContainerHeader 
			{

				public ContainerHeader()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "ContainerHeader";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ContainerHeader));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static ContainerHeader DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ContainerHeader));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (ContainerHeader) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(ContainerHeader sourceObject)
				{
					Title = sourceObject.Title;
					SubTitle = sourceObject.SubTitle;
				}
				



			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public string SubTitle { get; set; }
			private string _unmodified_SubTitle;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class ActivitySummaryContainer 
			{

				public ActivitySummaryContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "ActivitySummaryContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ActivitySummaryContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static ActivitySummaryContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ActivitySummaryContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (ActivitySummaryContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(ActivitySummaryContainer sourceObject)
				{
					Header = sourceObject.Header;
					SummaryBody = sourceObject.SummaryBody;
					Introduction = sourceObject.Introduction;
					ActivityIndex = sourceObject.ActivityIndex;
					ActivityCollection = sourceObject.ActivityCollection;
				}
				



			[DataMember]
			public ContainerHeader Header { get; set; }
			private ContainerHeader _unmodified_Header;
			[DataMember]
			public string SummaryBody { get; set; }
			private string _unmodified_SummaryBody;
			[DataMember]
			public Introduction Introduction { get; set; }
			private Introduction _unmodified_Introduction;
			[DataMember]
			public ActivityIndex ActivityIndex { get; set; }
			private ActivityIndex _unmodified_ActivityIndex;
			[DataMember]
			public ActivityCollection ActivityCollection { get; set; }
			private ActivityCollection _unmodified_ActivityCollection;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class ActivityIndex 
			{

				public ActivityIndex()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "ActivityIndex";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ActivityIndex));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static ActivityIndex DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ActivityIndex));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (ActivityIndex) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(ActivityIndex sourceObject)
				{
					Icon = sourceObject.Icon;
					Title = sourceObject.Title;
					Introduction = sourceObject.Introduction;
					Summary = sourceObject.Summary;
				}
				



			[DataMember]
			public Image Icon { get; set; }
			private Image _unmodified_Icon;
			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public string Introduction { get; set; }
			private string _unmodified_Introduction;
			[DataMember]
			public string Summary { get; set; }
			private string _unmodified_Summary;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class ActivityContainer 
			{

				public ActivityContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "ActivityContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ActivityContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static ActivityContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ActivityContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (ActivityContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(ActivityContainer sourceObject)
				{
					Header = sourceObject.Header;
					ActivityIndex = sourceObject.ActivityIndex;
					ActivityModule = sourceObject.ActivityModule;
				}
				



			[DataMember]
			public ContainerHeader Header { get; set; }
			private ContainerHeader _unmodified_Header;
			[DataMember]
			public ActivityIndex ActivityIndex { get; set; }
			private ActivityIndex _unmodified_ActivityIndex;
			[DataMember]
			public Activity ActivityModule { get; set; }
			private Activity _unmodified_ActivityModule;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class ActivityCollection 
			{

				public ActivityCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "ActivityCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ActivityCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static ActivityCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ActivityCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (ActivityCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<Activity> CollectionContent = new List<Activity>();
				private Activity[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public Activity[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(ActivityCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Activity 
			{

				public Activity()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Activity";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Activity));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Activity DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Activity));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Activity) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Activity sourceObject)
				{
					ReferenceToInformation = sourceObject.ReferenceToInformation;
					ProfileImage = sourceObject.ProfileImage;
					IconImage = sourceObject.IconImage;
					ActivityName = sourceObject.ActivityName;
					Introduction = sourceObject.Introduction;
					ContactPerson = sourceObject.ContactPerson;
					StartingTime = sourceObject.StartingTime;
					Excerpt = sourceObject.Excerpt;
					Description = sourceObject.Description;
					IFrameSources = sourceObject.IFrameSources;
					Collaborators = sourceObject.Collaborators;
					ImageGroupCollection = sourceObject.ImageGroupCollection;
					LocationCollection = sourceObject.LocationCollection;
					CategoryCollection = sourceObject.CategoryCollection;
				}
				



			[DataMember]
			public ReferenceToInformation ReferenceToInformation { get; set; }
			private ReferenceToInformation _unmodified_ReferenceToInformation;
			[DataMember]
			public Image ProfileImage { get; set; }
			private Image _unmodified_ProfileImage;
			[DataMember]
			public Image IconImage { get; set; }
			private Image _unmodified_IconImage;
			[DataMember]
			public string ActivityName { get; set; }
			private string _unmodified_ActivityName;
			[DataMember]
			public Introduction Introduction { get; set; }
			private Introduction _unmodified_Introduction;
			[DataMember]
			public string ContactPerson { get; set; }
			private string _unmodified_ContactPerson;
			[DataMember]
			public DateTime StartingTime { get; set; }
			private DateTime _unmodified_StartingTime;
			[DataMember]
			public string Excerpt { get; set; }
			private string _unmodified_Excerpt;
			[DataMember]
			public string Description { get; set; }
			private string _unmodified_Description;
			[DataMember]
			public string IFrameSources { get; set; }
			private string _unmodified_IFrameSources;
			[DataMember]
			public CollaboratorCollection Collaborators { get; set; }
			private CollaboratorCollection _unmodified_Collaborators;
			[DataMember]
			public ImageGroupCollection ImageGroupCollection { get; set; }
			private ImageGroupCollection _unmodified_ImageGroupCollection;
			[DataMember]
			public AddressAndLocationCollection LocationCollection { get; set; }
			private AddressAndLocationCollection _unmodified_LocationCollection;
			[DataMember]
			public CategoryCollection CategoryCollection { get; set; }
			private CategoryCollection _unmodified_CategoryCollection;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class ModeratorCollection 
			{

				public ModeratorCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "ModeratorCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ModeratorCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static ModeratorCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ModeratorCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (ModeratorCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<Moderator> CollectionContent = new List<Moderator>();
				private Moderator[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public Moderator[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(ModeratorCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Moderator 
			{

				public Moderator()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Moderator";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Moderator));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Moderator DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Moderator));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Moderator) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Moderator sourceObject)
				{
					ModeratorName = sourceObject.ModeratorName;
					ProfileUrl = sourceObject.ProfileUrl;
				}
				



			[DataMember]
			public string ModeratorName { get; set; }
			private string _unmodified_ModeratorName;
			[DataMember]
			public string ProfileUrl { get; set; }
			private string _unmodified_ProfileUrl;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class CollaboratorCollection 
			{

				public CollaboratorCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "CollaboratorCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(CollaboratorCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static CollaboratorCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(CollaboratorCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (CollaboratorCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<Collaborator> CollectionContent = new List<Collaborator>();
				private Collaborator[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public Collaborator[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(CollaboratorCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Collaborator 
			{

				public Collaborator()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Collaborator";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Collaborator));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Collaborator DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Collaborator));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Collaborator) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Collaborator sourceObject)
				{
					AccountID = sourceObject.AccountID;
					EmailAddress = sourceObject.EmailAddress;
					CollaboratorName = sourceObject.CollaboratorName;
					Role = sourceObject.Role;
					ProfileUrl = sourceObject.ProfileUrl;
				}
				



			[DataMember]
			public string AccountID { get; set; }
			private string _unmodified_AccountID;
			[DataMember]
			public string EmailAddress { get; set; }
			private string _unmodified_EmailAddress;
			[DataMember]
			public string CollaboratorName { get; set; }
			private string _unmodified_CollaboratorName;
			[DataMember]
			public string Role { get; set; }
			private string _unmodified_Role;
			[DataMember]
			public string ProfileUrl { get; set; }
			private string _unmodified_ProfileUrl;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class GroupSummaryContainer 
			{

				public GroupSummaryContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "GroupSummaryContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(GroupSummaryContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static GroupSummaryContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(GroupSummaryContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (GroupSummaryContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(GroupSummaryContainer sourceObject)
				{
					Header = sourceObject.Header;
					SummaryBody = sourceObject.SummaryBody;
					Introduction = sourceObject.Introduction;
					GroupSummaryIndex = sourceObject.GroupSummaryIndex;
					GroupCollection = sourceObject.GroupCollection;
				}
				



			[DataMember]
			public ContainerHeader Header { get; set; }
			private ContainerHeader _unmodified_Header;
			[DataMember]
			public string SummaryBody { get; set; }
			private string _unmodified_SummaryBody;
			[DataMember]
			public Introduction Introduction { get; set; }
			private Introduction _unmodified_Introduction;
			[DataMember]
			public GroupIndex GroupSummaryIndex { get; set; }
			private GroupIndex _unmodified_GroupSummaryIndex;
			[DataMember]
			public GroupCollection GroupCollection { get; set; }
			private GroupCollection _unmodified_GroupCollection;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class GroupContainer 
			{

				public GroupContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "GroupContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(GroupContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static GroupContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(GroupContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (GroupContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(GroupContainer sourceObject)
				{
					Header = sourceObject.Header;
					GroupIndex = sourceObject.GroupIndex;
					GroupProfile = sourceObject.GroupProfile;
					Collaborators = sourceObject.Collaborators;
					PendingCollaborators = sourceObject.PendingCollaborators;
					Activities = sourceObject.Activities;
					ImageGroupCollection = sourceObject.ImageGroupCollection;
					LocationCollection = sourceObject.LocationCollection;
				}
				



			[DataMember]
			public ContainerHeader Header { get; set; }
			private ContainerHeader _unmodified_Header;
			[DataMember]
			public GroupIndex GroupIndex { get; set; }
			private GroupIndex _unmodified_GroupIndex;
			[DataMember]
			public Group GroupProfile { get; set; }
			private Group _unmodified_GroupProfile;
			[DataMember]
			public CollaboratorCollection Collaborators { get; set; }
			private CollaboratorCollection _unmodified_Collaborators;
			[DataMember]
			public CollaboratorCollection PendingCollaborators { get; set; }
			private CollaboratorCollection _unmodified_PendingCollaborators;
			[DataMember]
			public ActivityCollection Activities { get; set; }
			private ActivityCollection _unmodified_Activities;
			[DataMember]
			public ImageGroupCollection ImageGroupCollection { get; set; }
			private ImageGroupCollection _unmodified_ImageGroupCollection;
			[DataMember]
			public AddressAndLocationCollection LocationCollection { get; set; }
			private AddressAndLocationCollection _unmodified_LocationCollection;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class GroupIndex 
			{

				public GroupIndex()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "GroupIndex";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(GroupIndex));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static GroupIndex DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(GroupIndex));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (GroupIndex) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(GroupIndex sourceObject)
				{
					Icon = sourceObject.Icon;
					Title = sourceObject.Title;
					Introduction = sourceObject.Introduction;
					Summary = sourceObject.Summary;
				}
				



			[DataMember]
			public Image Icon { get; set; }
			private Image _unmodified_Icon;
			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public string Introduction { get; set; }
			private string _unmodified_Introduction;
			[DataMember]
			public string Summary { get; set; }
			private string _unmodified_Summary;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class AddAddressAndLocationInfo 
			{

				public AddAddressAndLocationInfo()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "AddAddressAndLocationInfo";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AddAddressAndLocationInfo));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static AddAddressAndLocationInfo DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AddAddressAndLocationInfo));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (AddAddressAndLocationInfo) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(AddAddressAndLocationInfo sourceObject)
				{
					LocationName = sourceObject.LocationName;
				}
				



			[DataMember]
			public string LocationName { get; set; }
			private string _unmodified_LocationName;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class AddImageInfo 
			{

				public AddImageInfo()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "AddImageInfo";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AddImageInfo));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static AddImageInfo DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AddImageInfo));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (AddImageInfo) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(AddImageInfo sourceObject)
				{
					ImageTitle = sourceObject.ImageTitle;
				}
				



			[DataMember]
			public string ImageTitle { get; set; }
			private string _unmodified_ImageTitle;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class AddImageGroupInfo 
			{

				public AddImageGroupInfo()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "AddImageGroupInfo";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AddImageGroupInfo));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static AddImageGroupInfo DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AddImageGroupInfo));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (AddImageGroupInfo) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(AddImageGroupInfo sourceObject)
				{
					ImageGroupTitle = sourceObject.ImageGroupTitle;
				}
				



			[DataMember]
			public string ImageGroupTitle { get; set; }
			private string _unmodified_ImageGroupTitle;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class AddEmailAddressInfo 
			{

				public AddEmailAddressInfo()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "AddEmailAddressInfo";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AddEmailAddressInfo));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static AddEmailAddressInfo DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AddEmailAddressInfo));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (AddEmailAddressInfo) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(AddEmailAddressInfo sourceObject)
				{
					EmailAddress = sourceObject.EmailAddress;
				}
				



			[DataMember]
			public string EmailAddress { get; set; }
			private string _unmodified_EmailAddress;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class CreateGroupInfo 
			{

				public CreateGroupInfo()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "CreateGroupInfo";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(CreateGroupInfo));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static CreateGroupInfo DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(CreateGroupInfo));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (CreateGroupInfo) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(CreateGroupInfo sourceObject)
				{
					GroupName = sourceObject.GroupName;
				}
				



			[DataMember]
			public string GroupName { get; set; }
			private string _unmodified_GroupName;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class AddActivityInfo 
			{

				public AddActivityInfo()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "AddActivityInfo";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AddActivityInfo));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static AddActivityInfo DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AddActivityInfo));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (AddActivityInfo) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(AddActivityInfo sourceObject)
				{
					ActivityName = sourceObject.ActivityName;
				}
				



			[DataMember]
			public string ActivityName { get; set; }
			private string _unmodified_ActivityName;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class AddBlogPostInfo 
			{

				public AddBlogPostInfo()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "AddBlogPostInfo";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AddBlogPostInfo));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static AddBlogPostInfo DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AddBlogPostInfo));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (AddBlogPostInfo) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(AddBlogPostInfo sourceObject)
				{
					Title = sourceObject.Title;
				}
				



			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class AddCategoryInfo 
			{

				public AddCategoryInfo()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "AddCategoryInfo";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AddCategoryInfo));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static AddCategoryInfo DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AddCategoryInfo));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (AddCategoryInfo) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(AddCategoryInfo sourceObject)
				{
					CategoryName = sourceObject.CategoryName;
				}
				



			[DataMember]
			public string CategoryName { get; set; }
			private string _unmodified_CategoryName;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class GroupCollection 
			{

				public GroupCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "GroupCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(GroupCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static GroupCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(GroupCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (GroupCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<Group> CollectionContent = new List<Group>();
				private Group[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public Group[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(GroupCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Group 
			{

				public Group()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Group";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Group));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Group DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Group));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Group) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Group sourceObject)
				{
					ReferenceToInformation = sourceObject.ReferenceToInformation;
					ProfileImage = sourceObject.ProfileImage;
					IconImage = sourceObject.IconImage;
					GroupName = sourceObject.GroupName;
					Description = sourceObject.Description;
					OrganizationsAndGroupsLinkedToUs = sourceObject.OrganizationsAndGroupsLinkedToUs;
					WwwSiteToPublishTo = sourceObject.WwwSiteToPublishTo;
					CustomUICollection = sourceObject.CustomUICollection;
					Moderators = sourceObject.Moderators;
					CategoryCollection = sourceObject.CategoryCollection;
				}
				



			[DataMember]
			public ReferenceToInformation ReferenceToInformation { get; set; }
			private ReferenceToInformation _unmodified_ReferenceToInformation;
			[DataMember]
			public Image ProfileImage { get; set; }
			private Image _unmodified_ProfileImage;
			[DataMember]
			public Image IconImage { get; set; }
			private Image _unmodified_IconImage;
			[DataMember]
			public string GroupName { get; set; }
			private string _unmodified_GroupName;
			[DataMember]
			public string Description { get; set; }
			private string _unmodified_Description;
			[DataMember]
			public string OrganizationsAndGroupsLinkedToUs { get; set; }
			private string _unmodified_OrganizationsAndGroupsLinkedToUs;
			[DataMember]
			public string WwwSiteToPublishTo { get; set; }
			private string _unmodified_WwwSiteToPublishTo;
			[DataMember]
			public ShortTextCollection CustomUICollection { get; set; }
			private ShortTextCollection _unmodified_CustomUICollection;
			[DataMember]
			public ModeratorCollection Moderators { get; set; }
			private ModeratorCollection _unmodified_Moderators;
			[DataMember]
			public CategoryCollection CategoryCollection { get; set; }
			private CategoryCollection _unmodified_CategoryCollection;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Introduction 
			{

				public Introduction()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Introduction";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Introduction));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Introduction DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Introduction));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Introduction) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Introduction sourceObject)
				{
					Title = sourceObject.Title;
					Body = sourceObject.Body;
				}
				



			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public string Body { get; set; }
			private string _unmodified_Body;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class ContentCategoryRankCollection 
			{

				public ContentCategoryRankCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "ContentCategoryRankCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ContentCategoryRankCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static ContentCategoryRankCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ContentCategoryRankCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (ContentCategoryRankCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<ContentCategoryRank> CollectionContent = new List<ContentCategoryRank>();
				private ContentCategoryRank[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public ContentCategoryRank[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(ContentCategoryRankCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class ContentCategoryRank 
			{

				public ContentCategoryRank()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "ContentCategoryRank";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ContentCategoryRank));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static ContentCategoryRank DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ContentCategoryRank));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (ContentCategoryRank) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(ContentCategoryRank sourceObject)
				{
					ContentID = sourceObject.ContentID;
					ContentSemanticType = sourceObject.ContentSemanticType;
					CategoryID = sourceObject.CategoryID;
					RankName = sourceObject.RankName;
					RankValue = sourceObject.RankValue;
				}
				



			[DataMember]
			public string ContentID { get; set; }
			private string _unmodified_ContentID;
			[DataMember]
			public string ContentSemanticType { get; set; }
			private string _unmodified_ContentSemanticType;
			[DataMember]
			public string CategoryID { get; set; }
			private string _unmodified_CategoryID;
			[DataMember]
			public string RankName { get; set; }
			private string _unmodified_RankName;
			[DataMember]
			public string RankValue { get; set; }
			private string _unmodified_RankValue;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class LinkToContentCollection 
			{

				public LinkToContentCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "LinkToContentCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(LinkToContentCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static LinkToContentCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(LinkToContentCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (LinkToContentCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<LinkToContent> CollectionContent = new List<LinkToContent>();
				private LinkToContent[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public LinkToContent[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(LinkToContentCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class LinkToContent 
			{

				public LinkToContent()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "LinkToContent";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(LinkToContent));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static LinkToContent DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(LinkToContent));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (LinkToContent) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(LinkToContent sourceObject)
				{
					URL = sourceObject.URL;
					Title = sourceObject.Title;
					Description = sourceObject.Description;
					Published = sourceObject.Published;
					Author = sourceObject.Author;
					ImageData = sourceObject.ImageData;
					Locations = sourceObject.Locations;
					Categories = sourceObject.Categories;
				}
				



			[DataMember]
			public string URL { get; set; }
			private string _unmodified_URL;
			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public string Description { get; set; }
			private string _unmodified_Description;
			[DataMember]
			public DateTime Published { get; set; }
			private DateTime _unmodified_Published;
			[DataMember]
			public string Author { get; set; }
			private string _unmodified_Author;
			[DataMember]
			public MediaContent ImageData { get; set; }
			private MediaContent _unmodified_ImageData;
			[DataMember]
			public AddressAndLocationCollection Locations { get; set; }
			private AddressAndLocationCollection _unmodified_Locations;
			[DataMember]
			public CategoryCollection Categories { get; set; }
			private CategoryCollection _unmodified_Categories;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class EmbeddedContentCollection 
			{

				public EmbeddedContentCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "EmbeddedContentCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(EmbeddedContentCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static EmbeddedContentCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(EmbeddedContentCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (EmbeddedContentCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<EmbeddedContent> CollectionContent = new List<EmbeddedContent>();
				private EmbeddedContent[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public EmbeddedContent[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(EmbeddedContentCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class EmbeddedContent 
			{

				public EmbeddedContent()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "EmbeddedContent";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(EmbeddedContent));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static EmbeddedContent DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(EmbeddedContent));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (EmbeddedContent) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(EmbeddedContent sourceObject)
				{
					IFrameTagContents = sourceObject.IFrameTagContents;
					Title = sourceObject.Title;
					Published = sourceObject.Published;
					Author = sourceObject.Author;
					Description = sourceObject.Description;
					Locations = sourceObject.Locations;
					Categories = sourceObject.Categories;
				}
				



			[DataMember]
			public string IFrameTagContents { get; set; }
			private string _unmodified_IFrameTagContents;
			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public DateTime Published { get; set; }
			private DateTime _unmodified_Published;
			[DataMember]
			public string Author { get; set; }
			private string _unmodified_Author;
			[DataMember]
			public string Description { get; set; }
			private string _unmodified_Description;
			[DataMember]
			public AddressAndLocationCollection Locations { get; set; }
			private AddressAndLocationCollection _unmodified_Locations;
			[DataMember]
			public CategoryCollection Categories { get; set; }
			private CategoryCollection _unmodified_Categories;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class DynamicContentGroupCollection 
			{

				public DynamicContentGroupCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "DynamicContentGroupCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(DynamicContentGroupCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static DynamicContentGroupCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(DynamicContentGroupCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (DynamicContentGroupCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<DynamicContentGroup> CollectionContent = new List<DynamicContentGroup>();
				private DynamicContentGroup[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public DynamicContentGroup[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(DynamicContentGroupCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class DynamicContentGroup 
			{

				public DynamicContentGroup()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "DynamicContentGroup";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(DynamicContentGroup));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static DynamicContentGroup DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(DynamicContentGroup));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (DynamicContentGroup) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(DynamicContentGroup sourceObject)
				{
					HostName = sourceObject.HostName;
					GroupHeader = sourceObject.GroupHeader;
					SortValue = sourceObject.SortValue;
					PageLocation = sourceObject.PageLocation;
					ContentItemNames = sourceObject.ContentItemNames;
				}
				



			[DataMember]
			public string HostName { get; set; }
			private string _unmodified_HostName;
			[DataMember]
			public string GroupHeader { get; set; }
			private string _unmodified_GroupHeader;
			[DataMember]
			public string SortValue { get; set; }
			private string _unmodified_SortValue;
			[DataMember]
			public string PageLocation { get; set; }
			private string _unmodified_PageLocation;
			[DataMember]
			public string ContentItemNames { get; set; }
			private string _unmodified_ContentItemNames;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class DynamicContentCollection 
			{

				public DynamicContentCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "DynamicContentCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(DynamicContentCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static DynamicContentCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(DynamicContentCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (DynamicContentCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<DynamicContent> CollectionContent = new List<DynamicContent>();
				private DynamicContent[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public DynamicContent[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(DynamicContentCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class DynamicContent 
			{

				public DynamicContent()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "DynamicContent";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(DynamicContent));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static DynamicContent DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(DynamicContent));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (DynamicContent) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(DynamicContent sourceObject)
				{
					HostName = sourceObject.HostName;
					ContentName = sourceObject.ContentName;
					Title = sourceObject.Title;
					Description = sourceObject.Description;
					ElementQuery = sourceObject.ElementQuery;
					Content = sourceObject.Content;
					RawContent = sourceObject.RawContent;
					ImageData = sourceObject.ImageData;
					IsEnabled = sourceObject.IsEnabled;
					ApplyActively = sourceObject.ApplyActively;
					EditType = sourceObject.EditType;
					PageLocation = sourceObject.PageLocation;
				}
				



			[DataMember]
			public string HostName { get; set; }
			private string _unmodified_HostName;
			[DataMember]
			public string ContentName { get; set; }
			private string _unmodified_ContentName;
			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public string Description { get; set; }
			private string _unmodified_Description;
			[DataMember]
			public string ElementQuery { get; set; }
			private string _unmodified_ElementQuery;
			[DataMember]
			public string Content { get; set; }
			private string _unmodified_Content;
			[DataMember]
			public string RawContent { get; set; }
			private string _unmodified_RawContent;
			[DataMember]
			public MediaContent ImageData { get; set; }
			private MediaContent _unmodified_ImageData;
			[DataMember]
			public bool IsEnabled { get; set; }
			private bool _unmodified_IsEnabled;
			[DataMember]
			public bool ApplyActively { get; set; }
			private bool _unmodified_ApplyActively;
			[DataMember]
			public string EditType { get; set; }
			private string _unmodified_EditType;
			[DataMember]
			public string PageLocation { get; set; }
			private string _unmodified_PageLocation;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class AttachedToObjectCollection 
			{

				public AttachedToObjectCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "AttachedToObjectCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AttachedToObjectCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static AttachedToObjectCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AttachedToObjectCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (AttachedToObjectCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<AttachedToObject> CollectionContent = new List<AttachedToObject>();
				private AttachedToObject[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public AttachedToObject[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(AttachedToObjectCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class AttachedToObject 
			{

				public AttachedToObject()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "AttachedToObject";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AttachedToObject));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static AttachedToObject DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AttachedToObject));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (AttachedToObject) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(AttachedToObject sourceObject)
				{
					SourceObjectID = sourceObject.SourceObjectID;
					SourceObjectName = sourceObject.SourceObjectName;
					SourceObjectDomain = sourceObject.SourceObjectDomain;
					TargetObjectID = sourceObject.TargetObjectID;
					TargetObjectName = sourceObject.TargetObjectName;
					TargetObjectDomain = sourceObject.TargetObjectDomain;
				}
				



			[DataMember]
			public string SourceObjectID { get; set; }
			private string _unmodified_SourceObjectID;
			[DataMember]
			public string SourceObjectName { get; set; }
			private string _unmodified_SourceObjectName;
			[DataMember]
			public string SourceObjectDomain { get; set; }
			private string _unmodified_SourceObjectDomain;
			[DataMember]
			public string TargetObjectID { get; set; }
			private string _unmodified_TargetObjectID;
			[DataMember]
			public string TargetObjectName { get; set; }
			private string _unmodified_TargetObjectName;
			[DataMember]
			public string TargetObjectDomain { get; set; }
			private string _unmodified_TargetObjectDomain;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class CommentCollection 
			{

				public CommentCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "CommentCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(CommentCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static CommentCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(CommentCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (CommentCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<Comment> CollectionContent = new List<Comment>();
				private Comment[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public Comment[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(CommentCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Comment 
			{

				public Comment()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Comment";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Comment));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Comment DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Comment));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Comment) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Comment sourceObject)
				{
					TargetObjectID = sourceObject.TargetObjectID;
					TargetObjectName = sourceObject.TargetObjectName;
					TargetObjectDomain = sourceObject.TargetObjectDomain;
					CommentText = sourceObject.CommentText;
					Created = sourceObject.Created;
					OriginalAuthorName = sourceObject.OriginalAuthorName;
					OriginalAuthorEmail = sourceObject.OriginalAuthorEmail;
					OriginalAuthorAccountID = sourceObject.OriginalAuthorAccountID;
					LastModified = sourceObject.LastModified;
					LastAuthorName = sourceObject.LastAuthorName;
					LastAuthorEmail = sourceObject.LastAuthorEmail;
					LastAuthorAccountID = sourceObject.LastAuthorAccountID;
				}
				



			[DataMember]
			public string TargetObjectID { get; set; }
			private string _unmodified_TargetObjectID;
			[DataMember]
			public string TargetObjectName { get; set; }
			private string _unmodified_TargetObjectName;
			[DataMember]
			public string TargetObjectDomain { get; set; }
			private string _unmodified_TargetObjectDomain;
			[DataMember]
			public string CommentText { get; set; }
			private string _unmodified_CommentText;
			[DataMember]
			public DateTime Created { get; set; }
			private DateTime _unmodified_Created;
			[DataMember]
			public string OriginalAuthorName { get; set; }
			private string _unmodified_OriginalAuthorName;
			[DataMember]
			public string OriginalAuthorEmail { get; set; }
			private string _unmodified_OriginalAuthorEmail;
			[DataMember]
			public string OriginalAuthorAccountID { get; set; }
			private string _unmodified_OriginalAuthorAccountID;
			[DataMember]
			public DateTime LastModified { get; set; }
			private DateTime _unmodified_LastModified;
			[DataMember]
			public string LastAuthorName { get; set; }
			private string _unmodified_LastAuthorName;
			[DataMember]
			public string LastAuthorEmail { get; set; }
			private string _unmodified_LastAuthorEmail;
			[DataMember]
			public string LastAuthorAccountID { get; set; }
			private string _unmodified_LastAuthorAccountID;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class SelectionCollection 
			{

				public SelectionCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "SelectionCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SelectionCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static SelectionCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SelectionCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (SelectionCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<Selection> CollectionContent = new List<Selection>();
				private Selection[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public Selection[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(SelectionCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Selection 
			{

				public Selection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Selection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Selection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Selection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Selection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Selection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Selection sourceObject)
				{
					TargetObjectID = sourceObject.TargetObjectID;
					TargetObjectName = sourceObject.TargetObjectName;
					TargetObjectDomain = sourceObject.TargetObjectDomain;
					SelectionCategory = sourceObject.SelectionCategory;
					TextValue = sourceObject.TextValue;
					BooleanValue = sourceObject.BooleanValue;
					DoubleValue = sourceObject.DoubleValue;
				}
				



			[DataMember]
			public string TargetObjectID { get; set; }
			private string _unmodified_TargetObjectID;
			[DataMember]
			public string TargetObjectName { get; set; }
			private string _unmodified_TargetObjectName;
			[DataMember]
			public string TargetObjectDomain { get; set; }
			private string _unmodified_TargetObjectDomain;
			[DataMember]
			public string SelectionCategory { get; set; }
			private string _unmodified_SelectionCategory;
			[DataMember]
			public string TextValue { get; set; }
			private string _unmodified_TextValue;
			[DataMember]
			public bool BooleanValue { get; set; }
			private bool _unmodified_BooleanValue;
			[DataMember]
			public double DoubleValue { get; set; }
			private double _unmodified_DoubleValue;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TextContentCollection 
			{

				public TextContentCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TextContentCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TextContentCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TextContentCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TextContentCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TextContentCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<TextContent> CollectionContent = new List<TextContent>();
				private TextContent[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public TextContent[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(TextContentCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class TextContent 
			{

				public TextContent()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "TextContent";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TextContent));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static TextContent DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TextContent));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (TextContent) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(TextContent sourceObject)
				{
					ImageData = sourceObject.ImageData;
					Title = sourceObject.Title;
					SubTitle = sourceObject.SubTitle;
					Published = sourceObject.Published;
					Author = sourceObject.Author;
					Excerpt = sourceObject.Excerpt;
					Body = sourceObject.Body;
					Locations = sourceObject.Locations;
					Categories = sourceObject.Categories;
					SortOrderNumber = sourceObject.SortOrderNumber;
					IFrameSources = sourceObject.IFrameSources;
					RawHtmlContent = sourceObject.RawHtmlContent;
				}
				



			[DataMember]
			public MediaContent ImageData { get; set; }
			private MediaContent _unmodified_ImageData;
			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public string SubTitle { get; set; }
			private string _unmodified_SubTitle;
			[DataMember]
			public DateTime Published { get; set; }
			private DateTime _unmodified_Published;
			[DataMember]
			public string Author { get; set; }
			private string _unmodified_Author;
			[DataMember]
			public string Excerpt { get; set; }
			private string _unmodified_Excerpt;
			[DataMember]
			public string Body { get; set; }
			private string _unmodified_Body;
			[DataMember]
			public AddressAndLocationCollection Locations { get; set; }
			private AddressAndLocationCollection _unmodified_Locations;
			[DataMember]
			public CategoryCollection Categories { get; set; }
			private CategoryCollection _unmodified_Categories;
			[DataMember]
			public double SortOrderNumber { get; set; }
			private double _unmodified_SortOrderNumber;
			[DataMember]
			public string IFrameSources { get; set; }
			private string _unmodified_IFrameSources;
			[DataMember]
			public string RawHtmlContent { get; set; }
			private string _unmodified_RawHtmlContent;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class BlogCollection 
			{

				public BlogCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "BlogCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(BlogCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static BlogCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(BlogCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (BlogCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<Blog> CollectionContent = new List<Blog>();
				private Blog[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public Blog[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(BlogCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Blog 
			{

				public Blog()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Blog";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Blog));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Blog DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Blog));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Blog) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Blog sourceObject)
				{
					ReferenceToInformation = sourceObject.ReferenceToInformation;
					ProfileImage = sourceObject.ProfileImage;
					IconImage = sourceObject.IconImage;
					Title = sourceObject.Title;
					SubTitle = sourceObject.SubTitle;
					Introduction = sourceObject.Introduction;
					Published = sourceObject.Published;
					Author = sourceObject.Author;
					FeaturedImage = sourceObject.FeaturedImage;
					ImageGroupCollection = sourceObject.ImageGroupCollection;
					VideoGroup = sourceObject.VideoGroup;
					Body = sourceObject.Body;
					Excerpt = sourceObject.Excerpt;
					IFrameSources = sourceObject.IFrameSources;
					LocationCollection = sourceObject.LocationCollection;
					CategoryCollection = sourceObject.CategoryCollection;
					SocialPanel = sourceObject.SocialPanel;
				}
				



			[DataMember]
			public ReferenceToInformation ReferenceToInformation { get; set; }
			private ReferenceToInformation _unmodified_ReferenceToInformation;
			[DataMember]
			public Image ProfileImage { get; set; }
			private Image _unmodified_ProfileImage;
			[DataMember]
			public Image IconImage { get; set; }
			private Image _unmodified_IconImage;
			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public string SubTitle { get; set; }
			private string _unmodified_SubTitle;
			[DataMember]
			public Introduction Introduction { get; set; }
			private Introduction _unmodified_Introduction;
			[DataMember]
			public DateTime Published { get; set; }
			private DateTime _unmodified_Published;
			[DataMember]
			public string Author { get; set; }
			private string _unmodified_Author;
			[DataMember]
			public Image FeaturedImage { get; set; }
			private Image _unmodified_FeaturedImage;
			[DataMember]
			public ImageGroupCollection ImageGroupCollection { get; set; }
			private ImageGroupCollection _unmodified_ImageGroupCollection;
			[DataMember]
			public VideoGroup VideoGroup { get; set; }
			private VideoGroup _unmodified_VideoGroup;
			[DataMember]
			public string Body { get; set; }
			private string _unmodified_Body;
			[DataMember]
			public string Excerpt { get; set; }
			private string _unmodified_Excerpt;
			[DataMember]
			public string IFrameSources { get; set; }
			private string _unmodified_IFrameSources;
			[DataMember]
			public AddressAndLocationCollection LocationCollection { get; set; }
			private AddressAndLocationCollection _unmodified_LocationCollection;
			[DataMember]
			public CategoryCollection CategoryCollection { get; set; }
			private CategoryCollection _unmodified_CategoryCollection;
			[DataMember]
			public SocialPanelCollection SocialPanel { get; set; }
			private SocialPanelCollection _unmodified_SocialPanel;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class BlogIndexGroup 
			{

				public BlogIndexGroup()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "BlogIndexGroup";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(BlogIndexGroup));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static BlogIndexGroup DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(BlogIndexGroup));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (BlogIndexGroup) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(BlogIndexGroup sourceObject)
				{
					Icon = sourceObject.Icon;
					Title = sourceObject.Title;
					Introduction = sourceObject.Introduction;
					GroupedByDate = sourceObject.GroupedByDate;
					GroupedByLocation = sourceObject.GroupedByLocation;
					GroupedByAuthor = sourceObject.GroupedByAuthor;
					GroupedByCategory = sourceObject.GroupedByCategory;
					FullBlogArchive = sourceObject.FullBlogArchive;
					BlogSourceForSummary = sourceObject.BlogSourceForSummary;
					Summary = sourceObject.Summary;
				}
				



			[DataMember]
			public Image Icon { get; set; }
			private Image _unmodified_Icon;
			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public string Introduction { get; set; }
			private string _unmodified_Introduction;
			[DataMember]
			public GroupedInformationCollection GroupedByDate { get; set; }
			private GroupedInformationCollection _unmodified_GroupedByDate;
			[DataMember]
			public GroupedInformationCollection GroupedByLocation { get; set; }
			private GroupedInformationCollection _unmodified_GroupedByLocation;
			[DataMember]
			public GroupedInformationCollection GroupedByAuthor { get; set; }
			private GroupedInformationCollection _unmodified_GroupedByAuthor;
			[DataMember]
			public GroupedInformationCollection GroupedByCategory { get; set; }
			private GroupedInformationCollection _unmodified_GroupedByCategory;
			[DataMember]
			public ReferenceCollection FullBlogArchive { get; set; }
			private ReferenceCollection _unmodified_FullBlogArchive;
			[DataMember]
			public BlogCollection BlogSourceForSummary { get; set; }
			private BlogCollection _unmodified_BlogSourceForSummary;
			[DataMember]
			public string Summary { get; set; }
			private string _unmodified_Summary;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class CalendarIndex 
			{

				public CalendarIndex()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "CalendarIndex";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(CalendarIndex));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static CalendarIndex DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(CalendarIndex));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (CalendarIndex) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(CalendarIndex sourceObject)
				{
					Icon = sourceObject.Icon;
					Title = sourceObject.Title;
					Introduction = sourceObject.Introduction;
					Summary = sourceObject.Summary;
				}
				



			[DataMember]
			public Image Icon { get; set; }
			private Image _unmodified_Icon;
			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public string Introduction { get; set; }
			private string _unmodified_Introduction;
			[DataMember]
			public string Summary { get; set; }
			private string _unmodified_Summary;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Filter 
			{

				public Filter()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Filter";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Filter));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Filter DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Filter));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Filter) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Filter sourceObject)
				{
					Title = sourceObject.Title;
				}
				



			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Calendar 
			{

				public Calendar()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Calendar";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Calendar));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Calendar DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Calendar));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Calendar) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Calendar sourceObject)
				{
					Title = sourceObject.Title;
				}
				



			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class CalendarCollection 
			{

				public CalendarCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "CalendarCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(CalendarCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static CalendarCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(CalendarCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (CalendarCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<Calendar> CollectionContent = new List<Calendar>();
				private Calendar[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public Calendar[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(CalendarCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Map 
			{

				public Map()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Map";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Map));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Map DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Map));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Map) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Map sourceObject)
				{
					Title = sourceObject.Title;
				}
				



			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class MapCollection 
			{

				public MapCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "MapCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(MapCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static MapCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(MapCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (MapCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<Map> CollectionContent = new List<Map>();
				private Map[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public Map[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(MapCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class MapIndexCollection 
			{

				public MapIndexCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "MapIndexCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(MapIndexCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static MapIndexCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(MapIndexCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (MapIndexCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(MapIndexCollection sourceObject)
				{
					MapByDate = sourceObject.MapByDate;
					MapByLocation = sourceObject.MapByLocation;
					MapByAuthor = sourceObject.MapByAuthor;
					MapByCategory = sourceObject.MapByCategory;
				}
				



			[DataMember]
			public MapCollection MapByDate { get; set; }
			private MapCollection _unmodified_MapByDate;
			[DataMember]
			public MapCollection MapByLocation { get; set; }
			private MapCollection _unmodified_MapByLocation;
			[DataMember]
			public MapCollection MapByAuthor { get; set; }
			private MapCollection _unmodified_MapByAuthor;
			[DataMember]
			public MapCollection MapByCategory { get; set; }
			private MapCollection _unmodified_MapByCategory;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class MapResult 
			{

				public MapResult()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "MapResult";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(MapResult));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static MapResult DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(MapResult));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (MapResult) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(MapResult sourceObject)
				{
					Location = sourceObject.Location;
				}
				



			[DataMember]
			public Location Location { get; set; }
			private Location _unmodified_Location;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class MapResultCollection 
			{

				public MapResultCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "MapResultCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(MapResultCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static MapResultCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(MapResultCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (MapResultCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<MapResult> CollectionContent = new List<MapResult>();
				private MapResult[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public MapResult[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(MapResultCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class MapResultsCollection 
			{

				public MapResultsCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "MapResultsCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(MapResultsCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static MapResultsCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(MapResultsCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (MapResultsCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(MapResultsCollection sourceObject)
				{
					ResultByDate = sourceObject.ResultByDate;
					ResultByAuthor = sourceObject.ResultByAuthor;
					ResultByProximity = sourceObject.ResultByProximity;
				}
				



			[DataMember]
			public MapResultCollection ResultByDate { get; set; }
			private MapResultCollection _unmodified_ResultByDate;
			[DataMember]
			public MapResultCollection ResultByAuthor { get; set; }
			private MapResultCollection _unmodified_ResultByAuthor;
			[DataMember]
			public MapResultCollection ResultByProximity { get; set; }
			private MapResultCollection _unmodified_ResultByProximity;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Video 
			{

				public Video()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Video";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Video));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Video DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Video));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Video) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Video sourceObject)
				{
					VideoData = sourceObject.VideoData;
					Title = sourceObject.Title;
					Caption = sourceObject.Caption;
				}
				



			[DataMember]
			public MediaContent VideoData { get; set; }
			private MediaContent _unmodified_VideoData;
			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public string Caption { get; set; }
			private string _unmodified_Caption;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class ImageCollection 
			{

				public ImageCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "ImageCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ImageCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static ImageCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ImageCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (ImageCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<Image> CollectionContent = new List<Image>();
				private Image[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public Image[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(ImageCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Image 
			{

				public Image()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Image";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Image));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Image DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Image));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Image) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Image sourceObject)
				{
					ReferenceToInformation = sourceObject.ReferenceToInformation;
					ImageData = sourceObject.ImageData;
					Title = sourceObject.Title;
					Caption = sourceObject.Caption;
					Description = sourceObject.Description;
					Locations = sourceObject.Locations;
					Categories = sourceObject.Categories;
				}
				



			[DataMember]
			public ReferenceToInformation ReferenceToInformation { get; set; }
			private ReferenceToInformation _unmodified_ReferenceToInformation;
			[DataMember]
			public MediaContent ImageData { get; set; }
			private MediaContent _unmodified_ImageData;
			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public string Caption { get; set; }
			private string _unmodified_Caption;
			[DataMember]
			public string Description { get; set; }
			private string _unmodified_Description;
			[DataMember]
			public AddressAndLocationCollection Locations { get; set; }
			private AddressAndLocationCollection _unmodified_Locations;
			[DataMember]
			public CategoryCollection Categories { get; set; }
			private CategoryCollection _unmodified_Categories;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class BinaryFileCollection 
			{

				public BinaryFileCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "BinaryFileCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(BinaryFileCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static BinaryFileCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(BinaryFileCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (BinaryFileCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<BinaryFile> CollectionContent = new List<BinaryFile>();
				private BinaryFile[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public BinaryFile[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(BinaryFileCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class BinaryFile 
			{

				public BinaryFile()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "BinaryFile";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(BinaryFile));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static BinaryFile DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(BinaryFile));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (BinaryFile) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(BinaryFile sourceObject)
				{
					OriginalFileName = sourceObject.OriginalFileName;
					Data = sourceObject.Data;
					Title = sourceObject.Title;
					Description = sourceObject.Description;
					Categories = sourceObject.Categories;
				}
				



			[DataMember]
			public string OriginalFileName { get; set; }
			private string _unmodified_OriginalFileName;
			[DataMember]
			public MediaContent Data { get; set; }
			private MediaContent _unmodified_Data;
			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public string Description { get; set; }
			private string _unmodified_Description;
			[DataMember]
			public CategoryCollection Categories { get; set; }
			private CategoryCollection _unmodified_Categories;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class MediaContent 
			{

				public MediaContent()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "MediaContent";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(MediaContent));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static MediaContent DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(MediaContent));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (MediaContent) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class ImageGroupCollection 
			{

				public ImageGroupCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "ImageGroupCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ImageGroupCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static ImageGroupCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ImageGroupCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (ImageGroupCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<ImageGroup> CollectionContent = new List<ImageGroup>();
				private ImageGroup[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public ImageGroup[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(ImageGroupCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class ImageGroup 
			{

				public ImageGroup()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "ImageGroup";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ImageGroup));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static ImageGroup DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ImageGroup));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (ImageGroup) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(ImageGroup sourceObject)
				{
					ReferenceToInformation = sourceObject.ReferenceToInformation;
					Title = sourceObject.Title;
					Description = sourceObject.Description;
					FeaturedImage = sourceObject.FeaturedImage;
					ImagesCollection = sourceObject.ImagesCollection;
				}
				



			[DataMember]
			public ReferenceToInformation ReferenceToInformation { get; set; }
			private ReferenceToInformation _unmodified_ReferenceToInformation;
			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public string Description { get; set; }
			private string _unmodified_Description;
			[DataMember]
			public Image FeaturedImage { get; set; }
			private Image _unmodified_FeaturedImage;
			[DataMember]
			public ImageCollection ImagesCollection { get; set; }
			private ImageCollection _unmodified_ImagesCollection;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class VideoGroup 
			{

				public VideoGroup()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "VideoGroup";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(VideoGroup));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static VideoGroup DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(VideoGroup));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (VideoGroup) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(VideoGroup sourceObject)
				{
					Title = sourceObject.Title;
					Description = sourceObject.Description;
					VideoCollection = sourceObject.VideoCollection;
				}
				



			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public string Description { get; set; }
			private string _unmodified_Description;
			[DataMember]
			public VideoCollection VideoCollection { get; set; }
			private VideoCollection _unmodified_VideoCollection;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class VideoCollection 
			{

				public VideoCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "VideoCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(VideoCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static VideoCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(VideoCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (VideoCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<Video> CollectionContent = new List<Video>();
				private Video[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public Video[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(VideoCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Tooltip 
			{

				public Tooltip()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Tooltip";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Tooltip));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Tooltip DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Tooltip));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Tooltip) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Tooltip sourceObject)
				{
					TooltipText = sourceObject.TooltipText;
				}
				



			[DataMember]
			public string TooltipText { get; set; }
			private string _unmodified_TooltipText;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class SocialPanelCollection 
			{

				public SocialPanelCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "SocialPanelCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SocialPanelCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static SocialPanelCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SocialPanelCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (SocialPanelCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<SocialPanel> CollectionContent = new List<SocialPanel>();
				private SocialPanel[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public SocialPanel[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(SocialPanelCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class SocialPanel 
			{

				public SocialPanel()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "SocialPanel";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SocialPanel));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static SocialPanel DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SocialPanel));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (SocialPanel) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(SocialPanel sourceObject)
				{
					SocialFilter = sourceObject.SocialFilter;
				}
				



			[DataMember]
			public Filter SocialFilter { get; set; }
			private Filter _unmodified_SocialFilter;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Longitude 
			{

				public Longitude()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Longitude";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Longitude));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Longitude DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Longitude));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Longitude) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Longitude sourceObject)
				{
					TextValue = sourceObject.TextValue;
				}
				



			[DataMember]
			public string TextValue { get; set; }
			private string _unmodified_TextValue;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Latitude 
			{

				public Latitude()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Latitude";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Latitude));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Latitude DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Latitude));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Latitude) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Latitude sourceObject)
				{
					TextValue = sourceObject.TextValue;
				}
				



			[DataMember]
			public string TextValue { get; set; }
			private string _unmodified_TextValue;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Location 
			{

				public Location()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Location";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Location));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Location DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Location));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Location) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Location sourceObject)
				{
					LocationName = sourceObject.LocationName;
					Longitude = sourceObject.Longitude;
					Latitude = sourceObject.Latitude;
				}
				



			[DataMember]
			public string LocationName { get; set; }
			private string _unmodified_LocationName;
			[DataMember]
			public Longitude Longitude { get; set; }
			private Longitude _unmodified_Longitude;
			[DataMember]
			public Latitude Latitude { get; set; }
			private Latitude _unmodified_Latitude;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class LocationCollection 
			{

				public LocationCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "LocationCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(LocationCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static LocationCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(LocationCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (LocationCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<Location> CollectionContent = new List<Location>();
				private Location[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public Location[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(LocationCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Date 
			{

				public Date()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Date";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Date));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Date DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Date));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Date) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Date sourceObject)
				{
					Day = sourceObject.Day;
					Week = sourceObject.Week;
					Month = sourceObject.Month;
					Year = sourceObject.Year;
				}
				



			[DataMember]
			public DateTime Day { get; set; }
			private DateTime _unmodified_Day;
			[DataMember]
			public DateTime Week { get; set; }
			private DateTime _unmodified_Week;
			[DataMember]
			public DateTime Month { get; set; }
			private DateTime _unmodified_Month;
			[DataMember]
			public DateTime Year { get; set; }
			private DateTime _unmodified_Year;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Sex 
			{

				public Sex()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Sex";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Sex));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Sex DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Sex));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Sex) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Sex sourceObject)
				{
					SexText = sourceObject.SexText;
				}
				



			[DataMember]
			public string SexText { get; set; }
			private string _unmodified_SexText;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class OBSAddress 
			{

				public OBSAddress()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "OBSAddress";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(OBSAddress));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static OBSAddress DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(OBSAddress));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (OBSAddress) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(OBSAddress sourceObject)
				{
					StreetName = sourceObject.StreetName;
					BuildingNumber = sourceObject.BuildingNumber;
					PostOfficeBox = sourceObject.PostOfficeBox;
					PostalCode = sourceObject.PostalCode;
					Municipality = sourceObject.Municipality;
					Region = sourceObject.Region;
					Province = sourceObject.Province;
					state = sourceObject.state;
					Country = sourceObject.Country;
					Continent = sourceObject.Continent;
				}
				



			[DataMember]
			public string StreetName { get; set; }
			private string _unmodified_StreetName;
			[DataMember]
			public string BuildingNumber { get; set; }
			private string _unmodified_BuildingNumber;
			[DataMember]
			public string PostOfficeBox { get; set; }
			private string _unmodified_PostOfficeBox;
			[DataMember]
			public string PostalCode { get; set; }
			private string _unmodified_PostalCode;
			[DataMember]
			public string Municipality { get; set; }
			private string _unmodified_Municipality;
			[DataMember]
			public string Region { get; set; }
			private string _unmodified_Region;
			[DataMember]
			public string Province { get; set; }
			private string _unmodified_Province;
			[DataMember]
			public string state { get; set; }
			private string _unmodified_state;
			[DataMember]
			public string Country { get; set; }
			private string _unmodified_Country;
			[DataMember]
			public string Continent { get; set; }
			private string _unmodified_Continent;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Identity 
			{

				public Identity()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Identity";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Identity));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Identity DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Identity));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Identity) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Identity sourceObject)
				{
					FirstName = sourceObject.FirstName;
					LastName = sourceObject.LastName;
					Initials = sourceObject.Initials;
					Sex = sourceObject.Sex;
					Birthday = sourceObject.Birthday;
				}
				



			[DataMember]
			public string FirstName { get; set; }
			private string _unmodified_FirstName;
			[DataMember]
			public string LastName { get; set; }
			private string _unmodified_LastName;
			[DataMember]
			public string Initials { get; set; }
			private string _unmodified_Initials;
			[DataMember]
			public Sex Sex { get; set; }
			private Sex _unmodified_Sex;
			[DataMember]
			public Date Birthday { get; set; }
			private Date _unmodified_Birthday;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class ImageVideoSoundVectorRaw 
			{

				public ImageVideoSoundVectorRaw()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "ImageVideoSoundVectorRaw";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ImageVideoSoundVectorRaw));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static ImageVideoSoundVectorRaw DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(ImageVideoSoundVectorRaw));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (ImageVideoSoundVectorRaw) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(ImageVideoSoundVectorRaw sourceObject)
				{
					Image = sourceObject.Image;
					Video = sourceObject.Video;
					Sound = sourceObject.Sound;
					Vector = sourceObject.Vector;
					Raw = sourceObject.Raw;
				}
				



			[DataMember]
			public byte[] Image { get; set; }
			private byte[] _unmodified_Image;
			[DataMember]
			public byte[] Video { get; set; }
			private byte[] _unmodified_Video;
			[DataMember]
			public byte[] Sound { get; set; }
			private byte[] _unmodified_Sound;
			[DataMember]
			public string Vector { get; set; }
			private string _unmodified_Vector;
			[DataMember]
			public byte[] Raw { get; set; }
			private byte[] _unmodified_Raw;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class CategoryContainer 
			{

				public CategoryContainer()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "CategoryContainer";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(CategoryContainer));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static CategoryContainer DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(CategoryContainer));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (CategoryContainer) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(CategoryContainer sourceObject)
				{
					Categories = sourceObject.Categories;
				}
				



			[DataMember]
			public CategoryCollection Categories { get; set; }
			private CategoryCollection _unmodified_Categories;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Category 
			{

				public Category()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Category";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Category));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Category DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Category));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Category) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Category sourceObject)
				{
					ReferenceToInformation = sourceObject.ReferenceToInformation;
					CategoryName = sourceObject.CategoryName;
					ImageData = sourceObject.ImageData;
					Title = sourceObject.Title;
					Excerpt = sourceObject.Excerpt;
					ParentCategory = sourceObject.ParentCategory;
					ParentCategoryID = sourceObject.ParentCategoryID;
				}
				



			[DataMember]
			public ReferenceToInformation ReferenceToInformation { get; set; }
			private ReferenceToInformation _unmodified_ReferenceToInformation;
			[DataMember]
			public string CategoryName { get; set; }
			private string _unmodified_CategoryName;
			[DataMember]
			public MediaContent ImageData { get; set; }
			private MediaContent _unmodified_ImageData;
			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public string Excerpt { get; set; }
			private string _unmodified_Excerpt;
			[DataMember]
			public Category ParentCategory { get; set; }
			private Category _unmodified_ParentCategory;
			[DataMember]
			public string ParentCategoryID { get; set; }
			private string _unmodified_ParentCategoryID;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class CategoryCollection 
			{

				public CategoryCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "CategoryCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(CategoryCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static CategoryCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(CategoryCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (CategoryCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<Category> CollectionContent = new List<Category>();
				private Category[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public Category[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(CategoryCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class SubscriptionCollection 
			{

				public SubscriptionCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "SubscriptionCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SubscriptionCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static SubscriptionCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SubscriptionCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (SubscriptionCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<Subscription> CollectionContent = new List<Subscription>();
				private Subscription[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public Subscription[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(SubscriptionCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Subscription 
			{

				public Subscription()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Subscription";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Subscription));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Subscription DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Subscription));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Subscription) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Subscription sourceObject)
				{
					Priority = sourceObject.Priority;
					TargetRelativeLocation = sourceObject.TargetRelativeLocation;
					TargetInformationObjectType = sourceObject.TargetInformationObjectType;
					SubscriberRelativeLocation = sourceObject.SubscriberRelativeLocation;
					SubscriberInformationObjectType = sourceObject.SubscriberInformationObjectType;
					SubscriptionType = sourceObject.SubscriptionType;
				}
				



			[DataMember]
			public long Priority { get; set; }
			private long _unmodified_Priority;
			[DataMember]
			public string TargetRelativeLocation { get; set; }
			private string _unmodified_TargetRelativeLocation;
			[DataMember]
			public string TargetInformationObjectType { get; set; }
			private string _unmodified_TargetInformationObjectType;
			[DataMember]
			public string SubscriberRelativeLocation { get; set; }
			private string _unmodified_SubscriberRelativeLocation;
			[DataMember]
			public string SubscriberInformationObjectType { get; set; }
			private string _unmodified_SubscriberInformationObjectType;
			[DataMember]
			public string SubscriptionType { get; set; }
			private string _unmodified_SubscriptionType;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class QueueEnvelope 
			{

				public QueueEnvelope()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "QueueEnvelope";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(QueueEnvelope));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static QueueEnvelope DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(QueueEnvelope));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (QueueEnvelope) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(QueueEnvelope sourceObject)
				{
					ActiveContainerName = sourceObject.ActiveContainerName;
					OwnerPrefix = sourceObject.OwnerPrefix;
					CurrentRetryCount = sourceObject.CurrentRetryCount;
					SingleOperation = sourceObject.SingleOperation;
					OrderDependentOperationSequence = sourceObject.OrderDependentOperationSequence;
					ErrorContent = sourceObject.ErrorContent;
				}
				



			[DataMember]
			public string ActiveContainerName { get; set; }
			private string _unmodified_ActiveContainerName;
			[DataMember]
			public string OwnerPrefix { get; set; }
			private string _unmodified_OwnerPrefix;
			[DataMember]
			public long CurrentRetryCount { get; set; }
			private long _unmodified_CurrentRetryCount;
			[DataMember]
			public OperationRequest SingleOperation { get; set; }
			private OperationRequest _unmodified_SingleOperation;
			[DataMember]
			public OperationRequestCollection OrderDependentOperationSequence { get; set; }
			private OperationRequestCollection _unmodified_OrderDependentOperationSequence;
			[DataMember]
			public SystemError ErrorContent { get; set; }
			private SystemError _unmodified_ErrorContent;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class OperationRequestCollection 
			{

				public OperationRequestCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "OperationRequestCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(OperationRequestCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static OperationRequestCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(OperationRequestCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (OperationRequestCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<OperationRequest> CollectionContent = new List<OperationRequest>();
				private OperationRequest[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public OperationRequest[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(OperationRequestCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class OperationRequest 
			{

				public OperationRequest()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "OperationRequest";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(OperationRequest));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static OperationRequest DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(OperationRequest));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (OperationRequest) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(OperationRequest sourceObject)
				{
					SubscriberNotification = sourceObject.SubscriberNotification;
					SubscriptionChainRequest = sourceObject.SubscriptionChainRequest;
					UpdateWebContentOperation = sourceObject.UpdateWebContentOperation;
					RefreshDefaultViewsOperation = sourceObject.RefreshDefaultViewsOperation;
					DeleteEntireOwner = sourceObject.DeleteEntireOwner;
					DeleteOwnerContent = sourceObject.DeleteOwnerContent;
					PublishWebContent = sourceObject.PublishWebContent;
					ProcessIDToExecute = sourceObject.ProcessIDToExecute;
				}
				



			[DataMember]
			public Subscription SubscriberNotification { get; set; }
			private Subscription _unmodified_SubscriberNotification;
			[DataMember]
			public SubscriptionChainRequestMessage SubscriptionChainRequest { get; set; }
			private SubscriptionChainRequestMessage _unmodified_SubscriptionChainRequest;
			[DataMember]
			public UpdateWebContentOperation UpdateWebContentOperation { get; set; }
			private UpdateWebContentOperation _unmodified_UpdateWebContentOperation;
			[DataMember]
			public RefreshDefaultViewsOperation RefreshDefaultViewsOperation { get; set; }
			private RefreshDefaultViewsOperation _unmodified_RefreshDefaultViewsOperation;
			[DataMember]
			public DeleteEntireOwnerOperation DeleteEntireOwner { get; set; }
			private DeleteEntireOwnerOperation _unmodified_DeleteEntireOwner;
			[DataMember]
			public DeleteOwnerContentOperation DeleteOwnerContent { get; set; }
			private DeleteOwnerContentOperation _unmodified_DeleteOwnerContent;
			[DataMember]
			public PublishWebContentOperation PublishWebContent { get; set; }
			private PublishWebContentOperation _unmodified_PublishWebContent;
			[DataMember]
			public string ProcessIDToExecute { get; set; }
			private string _unmodified_ProcessIDToExecute;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class SubscriptionChainRequestMessage 
			{

				public SubscriptionChainRequestMessage()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "SubscriptionChainRequestMessage";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SubscriptionChainRequestMessage));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static SubscriptionChainRequestMessage DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SubscriptionChainRequestMessage));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (SubscriptionChainRequestMessage) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(SubscriptionChainRequestMessage sourceObject)
				{
					ContentItemID = sourceObject.ContentItemID;
				}
				



			[DataMember]
			public string ContentItemID { get; set; }
			private string _unmodified_ContentItemID;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class SubscriptionChainRequestContent 
			{

				public SubscriptionChainRequestContent()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "SubscriptionChainRequestContent";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SubscriptionChainRequestContent));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static SubscriptionChainRequestContent DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SubscriptionChainRequestContent));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (SubscriptionChainRequestContent) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(SubscriptionChainRequestContent sourceObject)
				{
					SubmitTime = sourceObject.SubmitTime;
					ProcessingStartTime = sourceObject.ProcessingStartTime;
					ProcessingEndTimeInformationObjects = sourceObject.ProcessingEndTimeInformationObjects;
					ProcessingEndTimeWebTemplatesRendering = sourceObject.ProcessingEndTimeWebTemplatesRendering;
					ProcessingEndTime = sourceObject.ProcessingEndTime;
					SubscriptionTargetCollection = sourceObject.SubscriptionTargetCollection;
				}
				



			[DataMember]
			public DateTime SubmitTime { get; set; }
			private DateTime _unmodified_SubmitTime;
			[DataMember]
			public DateTime ProcessingStartTime { get; set; }
			private DateTime _unmodified_ProcessingStartTime;
			[DataMember]
			public DateTime ProcessingEndTimeInformationObjects { get; set; }
			private DateTime _unmodified_ProcessingEndTimeInformationObjects;
			[DataMember]
			public DateTime ProcessingEndTimeWebTemplatesRendering { get; set; }
			private DateTime _unmodified_ProcessingEndTimeWebTemplatesRendering;
			[DataMember]
			public DateTime ProcessingEndTime { get; set; }
			private DateTime _unmodified_ProcessingEndTime;
			[DataMember]
			public SubscriptionTargetCollection SubscriptionTargetCollection { get; set; }
			private SubscriptionTargetCollection _unmodified_SubscriptionTargetCollection;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class SubscriptionTarget 
			{

				public SubscriptionTarget()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "SubscriptionTarget";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SubscriptionTarget));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static SubscriptionTarget DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SubscriptionTarget));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (SubscriptionTarget) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(SubscriptionTarget sourceObject)
				{
					BlobLocation = sourceObject.BlobLocation;
				}
				



			[DataMember]
			public string BlobLocation { get; set; }
			private string _unmodified_BlobLocation;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class SubscriptionTargetCollection 
			{

				public SubscriptionTargetCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "SubscriptionTargetCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SubscriptionTargetCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static SubscriptionTargetCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SubscriptionTargetCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (SubscriptionTargetCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<SubscriptionTarget> CollectionContent = new List<SubscriptionTarget>();
				private SubscriptionTarget[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public SubscriptionTarget[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(SubscriptionTargetCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class DeleteEntireOwnerOperation 
			{

				public DeleteEntireOwnerOperation()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "DeleteEntireOwnerOperation";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(DeleteEntireOwnerOperation));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static DeleteEntireOwnerOperation DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(DeleteEntireOwnerOperation));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (DeleteEntireOwnerOperation) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(DeleteEntireOwnerOperation sourceObject)
				{
					ContainerName = sourceObject.ContainerName;
					LocationPrefix = sourceObject.LocationPrefix;
				}
				



			[DataMember]
			public string ContainerName { get; set; }
			private string _unmodified_ContainerName;
			[DataMember]
			public string LocationPrefix { get; set; }
			private string _unmodified_LocationPrefix;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class DeleteOwnerContentOperation 
			{

				public DeleteOwnerContentOperation()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "DeleteOwnerContentOperation";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(DeleteOwnerContentOperation));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static DeleteOwnerContentOperation DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(DeleteOwnerContentOperation));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (DeleteOwnerContentOperation) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(DeleteOwnerContentOperation sourceObject)
				{
					ContainerName = sourceObject.ContainerName;
					LocationPrefix = sourceObject.LocationPrefix;
				}
				



			[DataMember]
			public string ContainerName { get; set; }
			private string _unmodified_ContainerName;
			[DataMember]
			public string LocationPrefix { get; set; }
			private string _unmodified_LocationPrefix;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class SystemError 
			{

				public SystemError()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "SystemError";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SystemError));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static SystemError DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SystemError));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (SystemError) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(SystemError sourceObject)
				{
					ErrorTitle = sourceObject.ErrorTitle;
					OccurredAt = sourceObject.OccurredAt;
					SystemErrorItems = sourceObject.SystemErrorItems;
					MessageContent = sourceObject.MessageContent;
				}
				



			[DataMember]
			public string ErrorTitle { get; set; }
			private string _unmodified_ErrorTitle;
			[DataMember]
			public DateTime OccurredAt { get; set; }
			private DateTime _unmodified_OccurredAt;
			[DataMember]
			public SystemErrorItemCollection SystemErrorItems { get; set; }
			private SystemErrorItemCollection _unmodified_SystemErrorItems;
			[DataMember]
			public QueueEnvelope MessageContent { get; set; }
			private QueueEnvelope _unmodified_MessageContent;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class SystemErrorItem 
			{

				public SystemErrorItem()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "SystemErrorItem";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SystemErrorItem));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static SystemErrorItem DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SystemErrorItem));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (SystemErrorItem) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(SystemErrorItem sourceObject)
				{
					ShortDescription = sourceObject.ShortDescription;
					LongDescription = sourceObject.LongDescription;
				}
				



			[DataMember]
			public string ShortDescription { get; set; }
			private string _unmodified_ShortDescription;
			[DataMember]
			public string LongDescription { get; set; }
			private string _unmodified_LongDescription;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class SystemErrorItemCollection 
			{

				public SystemErrorItemCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "SystemErrorItemCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SystemErrorItemCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static SystemErrorItemCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SystemErrorItemCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (SystemErrorItemCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<SystemErrorItem> CollectionContent = new List<SystemErrorItem>();
				private SystemErrorItem[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public SystemErrorItem[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(SystemErrorItemCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class InformationSource 
			{

				public InformationSource()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "InformationSource";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(InformationSource));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static InformationSource DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(InformationSource));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (InformationSource) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(InformationSource sourceObject)
				{
					SourceName = sourceObject.SourceName;
					SourceLocation = sourceObject.SourceLocation;
					SourceType = sourceObject.SourceType;
					IsDynamic = sourceObject.IsDynamic;
					SourceInformationObjectType = sourceObject.SourceInformationObjectType;
					SourceETag = sourceObject.SourceETag;
					SourceMD5 = sourceObject.SourceMD5;
					SourceLastModified = sourceObject.SourceLastModified;
				}
				



			[DataMember]
			public string SourceName { get; set; }
			private string _unmodified_SourceName;
			[DataMember]
			public string SourceLocation { get; set; }
			private string _unmodified_SourceLocation;
			[DataMember]
			public string SourceType { get; set; }
			private string _unmodified_SourceType;
			[DataMember]
			public bool IsDynamic { get; set; }
			private bool _unmodified_IsDynamic;
			[DataMember]
			public string SourceInformationObjectType { get; set; }
			private string _unmodified_SourceInformationObjectType;
			[DataMember]
			public string SourceETag { get; set; }
			private string _unmodified_SourceETag;
			[DataMember]
			public string SourceMD5 { get; set; }
			private string _unmodified_SourceMD5;
			[DataMember]
			public DateTime SourceLastModified { get; set; }
			private DateTime _unmodified_SourceLastModified;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class InformationSourceCollection 
			{

				public InformationSourceCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "InformationSourceCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(InformationSourceCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static InformationSourceCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(InformationSourceCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (InformationSourceCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<InformationSource> CollectionContent = new List<InformationSource>();
				private InformationSource[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public InformationSource[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(InformationSourceCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class RefreshDefaultViewsOperation 
			{

				public RefreshDefaultViewsOperation()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "RefreshDefaultViewsOperation";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(RefreshDefaultViewsOperation));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static RefreshDefaultViewsOperation DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(RefreshDefaultViewsOperation));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (RefreshDefaultViewsOperation) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(RefreshDefaultViewsOperation sourceObject)
				{
					ViewLocation = sourceObject.ViewLocation;
					TypeNameToRefresh = sourceObject.TypeNameToRefresh;
				}
				



			[DataMember]
			public string ViewLocation { get; set; }
			private string _unmodified_ViewLocation;
			[DataMember]
			public string TypeNameToRefresh { get; set; }
			private string _unmodified_TypeNameToRefresh;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class UpdateWebContentOperation 
			{

				public UpdateWebContentOperation()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "UpdateWebContentOperation";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(UpdateWebContentOperation));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static UpdateWebContentOperation DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(UpdateWebContentOperation));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (UpdateWebContentOperation) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(UpdateWebContentOperation sourceObject)
				{
					SourceContainerName = sourceObject.SourceContainerName;
					SourcePathRoot = sourceObject.SourcePathRoot;
					TargetContainerName = sourceObject.TargetContainerName;
					TargetPathRoot = sourceObject.TargetPathRoot;
					RenderWhileSync = sourceObject.RenderWhileSync;
					Handlers = sourceObject.Handlers;
				}
				



			[DataMember]
			public string SourceContainerName { get; set; }
			private string _unmodified_SourceContainerName;
			[DataMember]
			public string SourcePathRoot { get; set; }
			private string _unmodified_SourcePathRoot;
			[DataMember]
			public string TargetContainerName { get; set; }
			private string _unmodified_TargetContainerName;
			[DataMember]
			public string TargetPathRoot { get; set; }
			private string _unmodified_TargetPathRoot;
			[DataMember]
			public bool RenderWhileSync { get; set; }
			private bool _unmodified_RenderWhileSync;
			[DataMember]
			public UpdateWebContentHandlerCollection Handlers { get; set; }
			private UpdateWebContentHandlerCollection _unmodified_Handlers;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class UpdateWebContentHandlerItem 
			{

				public UpdateWebContentHandlerItem()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "UpdateWebContentHandlerItem";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(UpdateWebContentHandlerItem));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static UpdateWebContentHandlerItem DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(UpdateWebContentHandlerItem));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (UpdateWebContentHandlerItem) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(UpdateWebContentHandlerItem sourceObject)
				{
					InformationTypeName = sourceObject.InformationTypeName;
					OptionName = sourceObject.OptionName;
				}
				



			[DataMember]
			public string InformationTypeName { get; set; }
			private string _unmodified_InformationTypeName;
			[DataMember]
			public string OptionName { get; set; }
			private string _unmodified_OptionName;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class UpdateWebContentHandlerCollection 
			{

				public UpdateWebContentHandlerCollection()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "UpdateWebContentHandlerCollection";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(UpdateWebContentHandlerCollection));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static UpdateWebContentHandlerCollection DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(UpdateWebContentHandlerCollection));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (UpdateWebContentHandlerCollection) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }


				
				[DataMember] public List<UpdateWebContentHandlerItem> CollectionContent = new List<UpdateWebContentHandlerItem>();
				private UpdateWebContentHandlerItem[] _unmodified_CollectionContent;

				[DataMember] public bool IsCollectionFiltered;
				private bool _unmodified_IsCollectionFiltered;
				
				[DataMember] public List<string> OrderFilterIDList = new List<string>();
				private string[] _unmodified_OrderFilterIDList;

				public string SelectedIDCommaSeparated
				{
					get
					{
						string[] sourceArray;
						if (OrderFilterIDList != null)
							sourceArray = OrderFilterIDList.ToArray();
						else
							sourceArray = CollectionContent.Select(item => item.ID).ToArray();
						return String.Join(",", sourceArray);
					}
					set 
					{
						if (value == null)
							return;
						string[] valueArray = value.Split(',');
						OrderFilterIDList = new List<string>();
						OrderFilterIDList.AddRange(valueArray);
						OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
					}
				}

				public UpdateWebContentHandlerItem[] GetIDSelectedArray()
				{
					if (IsCollectionFiltered == false || this.OrderFilterIDList == null)
						return CollectionContent.ToArray();
					return
						this.OrderFilterIDList.Select(id => CollectionContent.FirstOrDefault(item => item.ID == id)).Where(item => item != null).ToArray();
				}

				public void RefreshOrderAndFilterListFromContent()
                {
                    if (OrderFilterIDList == null)
                        return;
                    OrderFilterIDList.RemoveAll(item => CollectionContent.Any(colItem => colItem.ID == item) == false);
                }

				private void CopyContentFrom(UpdateWebContentHandlerCollection sourceObject)
				{
					CollectionContent = sourceObject.CollectionContent;
					_unmodified_CollectionContent = sourceObject._unmodified_CollectionContent;
				}
				
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class PublishWebContentOperation 
			{

				public PublishWebContentOperation()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "PublishWebContentOperation";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(PublishWebContentOperation));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static PublishWebContentOperation DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(PublishWebContentOperation));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (PublishWebContentOperation) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(PublishWebContentOperation sourceObject)
				{
					SourceContainerName = sourceObject.SourceContainerName;
					SourcePathRoot = sourceObject.SourcePathRoot;
					SourceOwner = sourceObject.SourceOwner;
					TargetContainerName = sourceObject.TargetContainerName;
				}
				



			[DataMember]
			public string SourceContainerName { get; set; }
			private string _unmodified_SourceContainerName;
			[DataMember]
			public string SourcePathRoot { get; set; }
			private string _unmodified_SourcePathRoot;
			[DataMember]
			public string SourceOwner { get; set; }
			private string _unmodified_SourceOwner;
			[DataMember]
			public string TargetContainerName { get; set; }
			private string _unmodified_TargetContainerName;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class SubscriberInput 
			{

				public SubscriberInput()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "SubscriberInput";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SubscriberInput));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static SubscriberInput DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(SubscriberInput));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (SubscriberInput) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(SubscriberInput sourceObject)
				{
					InputRelativeLocation = sourceObject.InputRelativeLocation;
					InformationObjectName = sourceObject.InformationObjectName;
					InformationItemName = sourceObject.InformationItemName;
					SubscriberRelativeLocation = sourceObject.SubscriberRelativeLocation;
				}
				



			[DataMember]
			public string InputRelativeLocation { get; set; }
			private string _unmodified_InputRelativeLocation;
			[DataMember]
			public string InformationObjectName { get; set; }
			private string _unmodified_InformationObjectName;
			[DataMember]
			public string InformationItemName { get; set; }
			private string _unmodified_InformationItemName;
			[DataMember]
			public string SubscriberRelativeLocation { get; set; }
			private string _unmodified_SubscriberRelativeLocation;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class Monitor 
			{

				public Monitor()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "Monitor";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Monitor));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static Monitor DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(Monitor));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (Monitor) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(Monitor sourceObject)
				{
					TargetObjectName = sourceObject.TargetObjectName;
					TargetItemName = sourceObject.TargetItemName;
					MonitoringUtcTimeStampToStart = sourceObject.MonitoringUtcTimeStampToStart;
					MonitoringCycleFrequencyUnit = sourceObject.MonitoringCycleFrequencyUnit;
					MonitoringCycleEveryXthOfUnit = sourceObject.MonitoringCycleEveryXthOfUnit;
					CustomMonitoringCycleOperationName = sourceObject.CustomMonitoringCycleOperationName;
					OperationActionName = sourceObject.OperationActionName;
				}
				



			[DataMember]
			public string TargetObjectName { get; set; }
			private string _unmodified_TargetObjectName;
			[DataMember]
			public string TargetItemName { get; set; }
			private string _unmodified_TargetItemName;
			[DataMember]
			public DateTime MonitoringUtcTimeStampToStart { get; set; }
			private DateTime _unmodified_MonitoringUtcTimeStampToStart;
			[DataMember]
			public string MonitoringCycleFrequencyUnit { get; set; }
			private string _unmodified_MonitoringCycleFrequencyUnit;
			[DataMember]
			public long MonitoringCycleEveryXthOfUnit { get; set; }
			private long _unmodified_MonitoringCycleEveryXthOfUnit;
			[DataMember]
			public string CustomMonitoringCycleOperationName { get; set; }
			private string _unmodified_CustomMonitoringCycleOperationName;
			[DataMember]
			public string OperationActionName { get; set; }
			private string _unmodified_OperationActionName;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class IconTitleDescription 
			{

				public IconTitleDescription()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "IconTitleDescription";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(IconTitleDescription));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static IconTitleDescription DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(IconTitleDescription));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (IconTitleDescription) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(IconTitleDescription sourceObject)
				{
					Icon = sourceObject.Icon;
					Title = sourceObject.Title;
					Description = sourceObject.Description;
				}
				



			[DataMember]
			public byte[] Icon { get; set; }
			private byte[] _unmodified_Icon;
			[DataMember]
			public string Title { get; set; }
			private string _unmodified_Title;
			[DataMember]
			public string Description { get; set; }
			private string _unmodified_Description;
			
			}
            [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/AaltoGlobalImpact.OIP")]
			[Serializable]
			public partial class AboutAGIApplications 
			{

				public AboutAGIApplications()
				{
					this.ID = Guid.NewGuid().ToString();
				    this.SemanticDomainName = "AaltoGlobalImpact.OIP";
				    this.Name = "AboutAGIApplications";
				}


				public string SerializeToXml(bool noFormatting = false)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AboutAGIApplications));
					using (var output = new StringWriter())
					{
						using (var writer = new XmlTextWriter(output))
						{
                            if(noFormatting == false)
						        writer.Formatting = Formatting.Indented;
							serializer.WriteObject(writer, this);
						}
						return output.GetStringBuilder().ToString();
					}
				}

				public static AboutAGIApplications DeserializeFromXml(string xmlString)
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(AboutAGIApplications));
					using(StringReader reader = new StringReader(xmlString))
					{
						using (var xmlReader = new XmlTextReader(reader))
							return (AboutAGIApplications) serializer.ReadObject(xmlReader);
					}
            
				}

				[DataMember]
				public string ID { get; set; }

			    [IgnoreDataMember]
                public string ETag { get; set; }

                [DataMember]
                public Guid OwnerID { get; set; }

                [DataMember]
                public string RelativeLocation { get; set; }

                [DataMember]
                public string Name { get; set; }

                [DataMember]
                public string SemanticDomainName { get; set; }

				[DataMember]
				public string MasterETag { get; set; }

				[DataMember]
				public string GeneratedByProcessID { get; set; }



				private void CopyContentFrom(AboutAGIApplications sourceObject)
				{
					BuiltForAnybody = sourceObject.BuiltForAnybody;
					ForAllPeople = sourceObject.ForAllPeople;
				}
				



			[DataMember]
			public IconTitleDescription BuiltForAnybody { get; set; }
			private IconTitleDescription _unmodified_BuiltForAnybody;
			[DataMember]
			public IconTitleDescription ForAllPeople { get; set; }
			private IconTitleDescription _unmodified_ForAllPeople;
			
			}
 } 