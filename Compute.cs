using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

public class Compute
{
    internal static double ComputeCSV(string appID, StreamReader fileCSV)
	{
		var data = new List<string[]>();
		var appIDData = new List<string[]>();
		int row = 0;
		double finalLicenses = 0 ;

		while (!fileCSV.EndOfStream)
		{
			string[] Line = fileCSV.ReadLine().Split(',');
			data.Add(Line);
			row++;
		}

		appIDData = checkAppID(appID, data);

		finalLicenses = numOfLicenses(appIDData);

		return finalLicenses;
	}

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

			foreach(String[] lines in finaldata)
            {
				Console.WriteLine(lines[2]);
			}
			
		}
		catch (Exception ex)
		{
			MessageBox.Show($"Input error.\n\nError message: {ex.Message}\n");
		}

		return finaldata;
    }
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
            }
			else
            {
				numLicenses = 0;
            }
        }
		else
        {
			return numDesktops;
        }

		return numLicenses;

	}

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
