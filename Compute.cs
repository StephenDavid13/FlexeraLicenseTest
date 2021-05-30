using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

/* 
 * This generates all the necessary computations and forumalae for the license checker
 * @author sdavid
 * 
 */


public class Compute
{
	public class lCheck
    {
		public static int licChecker = 0;
    }

	public enum computerType
    {
		Desktop,
		Laptop
    };

    public static string ComputeCSV(string appID, StreamReader fileCSV)
	{
		var data = new List<string[]>();
		var appIDData = new List<string[]>();
		int row = 0;
		double finalLicenses = 0 ;

		/* Read CSV file */
		while (!fileCSV.EndOfStream)
		{
			string[] Line = fileCSV.ReadLine().Split(',');
			data.Add(Line);
			row++;
		}

		appIDData = checkAppID(appID, data);

		finalLicenses = numOfLicenses(appIDData);
		
		/* To check surplus or lacking licenses */
		if(lCheck.licChecker == 0)
        {
			return "Application ID: " + appID + " requires " + finalLicenses + " number of license(s).";
		}
		else
        {
			return "Application ID: " + appID + " has a surplus of " + finalLicenses + " number of license(s).";
		}
		
	}

	/* Check Application ID matching to CSV file */
	public static List<string[]> checkAppID(string appID, List<string[]> data)
    {
		List<string[]> finaldata = new List<string[]>();
		try
		{
			for (int i = 0; i < data.Count; i++)
			{
				if (data[i][2] == appID)
				{
					finaldata.Add(data[i]);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show($"Input error.\n\nError message: {ex.Message}\n");
		}

		return finaldata;
    }

	/* Check number of licenses */
	public static double numOfLicenses(List<string[]> dataLicense)
    {
		double numLicenses = 0;
		double numLaptops, numDesktops;

		numDesktops = numOfDevices(dataLicense, computerType.Desktop.ToString());
		numLaptops = numOfDevices(dataLicense, computerType.Laptop.ToString());

		if(numLaptops > 0)
        {
			double difference = numDesktops - numLaptops;

			if(difference > 0)
            {
				numLicenses = difference;
				lCheck.licChecker = 0;
            }
			else
            {
				numLicenses = numLaptops - numDesktops;
                lCheck.licChecker = 1;
            }
        }
		else
        {
			return numDesktops;
        }

		return numLicenses;

	}

	/* Check number of desktops */
	public static double numOfDevices(List<string[]> dataDevice, string computerType)
	{
		double numDevice = 0;
		foreach (String[] dataline in dataDevice)
		{
			if(dataline[3].ToUpper() == computerType.ToUpper())
            {
				numDevice++;
            }
		}
		return numDevice;
	}

}
