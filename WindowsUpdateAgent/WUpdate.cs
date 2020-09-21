using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsUpdateAgent;

namespace WindowsUpdateAgent
{
	public class WUpdate
	{
		// Identity > GUID
		public Identity Identity { get; set; }
		public string Title { get; set; }
		public string KBArticleID { get; set; }
		public string Description { get; set; }
		public bool EulaAccepted { get; set; }
		public bool RebootRequired { get; set; }
		public List<Identity> Superseded { get; set; }
	}

	public class Identity
	{
		//public int RevisionNumber { get; set; } removing revision for the time being
		public string UpdateID { get; set; }
	}

    public class WUpdateCollection
    {
        public List<WUpdate> WUpdateCol { get; set; }
    }



	/*
	-		Identity	{System.__ComObject
}
System.__ComObject
- Dynamic View Expanding the Dynamic View will get the dynamic members for the object	
		RevisionNumber	200	System.Int32
		UpdateID	"902a3559-18dc-4219-a6c6-818c55fe0a47"	System.String



		// SupersededUpdateIDs > GUIDs


		
         		AutoDownload	0	System.Int32
		AutoSelection	0	System.Int32
		AutoSelectOnWebSites	true	System.Boolean
		BrowseOnly	false	System.Boolean
+		BundledUpdates	{System.__ComObject}	System.__ComObject
		CanRequireSource	false	System.Boolean
+		Categories	{System.__ComObject}	System.__ComObject
+		CveIDs	{System.__ComObject}	System.__ComObject
		Deadline	null	<null>
		DeltaCompressedContentAvailable	false	System.Boolean
		DeltaCompressedContentPreferred	true	System.Boolean
		DeploymentAction	1	System.Int32
		Description	"Install this update to revise the files that are used to detect viruses, spyware, and other potentially unwanted software. Once you have installed this item, it cannot be removed."	System.String
+		DownloadContents	{System.__ComObject}	System.__ComObject
		DownloadPriority	2	System.Int32
		EulaAccepted	true	System.Boolean
		EulaText	null	<null>
		HandlerID	null	<null>
+		Identity	{System.__ComObject}	System.__ComObject
		Image	null	<null>
+		InstallationBehavior	{System.__ComObject}	System.__ComObject
		IsBeta	false	System.Boolean
		IsDownloaded	false	System.Boolean
		IsHidden	false	System.Boolean
		IsInstalled	false	System.Boolean
		IsMandatory	false	System.Boolean
		IsPresent	true	System.Boolean
		IsUninstallable	false	System.Boolean
+		KBArticleIDs	{System.__ComObject}	System.__ComObject
+		Languages	{System.__ComObject}	System.__ComObject
		LastDeploymentChangeTime	{13/09/2020 00:00:00}	System.DateTime
		MaxDownloadSize	675275528	System.Decimal
		MinDownloadSize	0	System.Decimal
+		MoreInfoUrls	{System.__ComObject}	System.__ComObject
		MsrcSeverity	null	<null>
		PerUser	false	System.Boolean
		RebootRequired	false	System.Boolean
		RecommendedCpuSpeed	0	System.Int32
		RecommendedHardDiskSpace	0	System.Int32
		RecommendedMemory	0	System.Int32
		ReleaseNotes	null	<null>
+		SecurityBulletinIDs	{System.__ComObject}	System.__ComObject
+		SupersededUpdateIDs	{System.__ComObject}	System.__ComObject
		SupportUrl	"https://go.microsoft.com/fwlink/?LinkId=52661"	System.String
		Title	"Security Intelligence Update for Microsoft Defender Antivirus - KB2267602 (Version 1.323.1092.0)"	System.String
		Type	1	System.Int32
		UninstallationBehavior	null	<null>
		UninstallationNotes	null	<null>
+		UninstallationSteps	{System.__ComObject}	System.__ComObject

         */

}