using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

public class Compute
{
	static class lCheck
    {
		public static int licChecker = 0;
    }

    internal static void ComputeCSV(string appID, StreamReader fileCSV)
	{
		var data = new List<string[]>();
		var appIDData = new List<string[]>();
		int row = 0;
		double finalLicenses = 0 ;

		/* Read CSV file */
		while (!fileCSV.EndOfStream)
		{
			string[] Line = fileCSV.ReadLine().Split(',');
			//Console.WriteLine(Line[0]);
			data.Add(Line);
			row++;
		}

		appIDData = checkAppID(appID, data);

		finalLicenses = numOfLicenses(appIDData);
		
		/* To check surplus or lacking licenses */
		if(lCheck.licChecker == 0)
        {
			MessageBox.Show("Application ID: " + appID + " requires " + finalLicenses + " number of license(s).");
		}
		else
        {
			MessageBox.Show("Application ID: " + appID + " has a surplus of " + finalLicenses + " number of license(s).");
		}
		
	}

	/* Check Application ID matching to CSV file */
	private static List<string[]> checkAppID(string appID, List<string[]> data)
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
	private static double numOfLicenses(List<string[]> dataLicense)
    {
		double numLicenses = 0;
		double numLaptops, numDesktops;

		numDesktops = numOfDesktops(dataLicense);
		numLaptops = numOfLaptops(dataLicense);

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
	private static double numOfDesktops(List<string[]> dataDesktop)
	{
		double numDesktop = 0;
		foreach (String[] dataline in dataDesktop)
		{
			if(dataline[3].ToUpper() == "DESKTOP")
            {
				numDesktop++;
            }
		}

		Console.WriteLine("No of Desktops: " + numDesktop);

		return numDesktop;
	}

	/* Check number of laptops */
	private static double numOfLaptops(List<string[]> dataLaptop)
	{
		double numLaptop = 0;
		foreach (String[] dataline in dataLaptop)
		{
			if (dataline[3].ToUpper() == "LAPTOP")
			{
				numLaptop++;
			}
		}

		Console.WriteLine("No of Laptops: " + numLaptop);

		return numLaptop;
	}

}
