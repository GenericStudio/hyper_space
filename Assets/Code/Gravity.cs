
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;


		                                                               
public class Gravity : MonoBehaviour
{
	private Hub hub;
		
	private List<GameObject> bodies = new List<GameObject>();

			
		// Use this for initialization
		void Start ()
		{
			hub = GameObject.Find ("hub").GetComponent<Hub> ();
			bodies = hub.latice.LaticeObjectManager;
		}
			
		// Update is called once per frame
		void FixedUpdate ()
		{
			Vector3 gravity = Vector3.zero;
			var self_point = hub.LaticeBox.transform.InverseTransformPoint(transform.position - rigidbody.velocity);
			foreach(GameObject body in bodies){
				Vector3 difference = new Vector3();
				var target_point =  hub.LaticeBox.transform.InverseTransformPoint(body.transform.position-body.rigidbody.velocity);
				difference = self_point-target_point;
				Vector3 direction = (difference);
				if(difference.x>0.5) direction.x = -direction.x;
				if(difference.y>0.5) direction.y = -direction.y;
				if(difference.z>0.5) direction.z = -direction.z;
				gravity-=direction * body.rigidbody.mass* (1/((body.rigidbody.velocity - rigidbody.velocity).magnitude+1));
			}
			rigidbody.AddForce(gravity);
		}
		

	
	public static int[] Cluster(double[][] rawData, int numClusters)
	{
		double[][] data = Normalized(rawData);
		bool changed = true; bool success = true;
		int[] clustering = InitClustering(data.Length, numClusters, 0);
		double[][] means = Allocate(numClusters, data[0].Length);
		int maxCount = data.Length * 10;
		int ct = 0;
		while (changed == true && success == true && ct < maxCount) {
			++ct;
			success = UpdateMeans(data, clustering, means);
			changed = UpdateClustering(data, clustering, means);
		}
		return clustering;
	}
	private static double[][] Normalized(double[][] rawData)
	{
		double[][] result = new double[rawData.Length][];
		for (int i = 0; i < rawData.Length; ++i)
		{
			result[i] = new double[rawData[i].Length];
			rawData[i].CopyTo(result[i],rawData[i].Length);
		//	Array.Copy(rawData[i], result[i], rawData[i].Length);
		}
		
		for (int j = 0; j < result[0].Length; ++j)
		{
			double colSum = 0.0;
			for (int i = 0; i < result.Length; ++i)
				colSum += result[i][j];
			double mean = colSum / result.Length;
			double sum = 0.0;
			for (int i = 0; i < result.Length; ++i)
				sum += (result[i][j] - mean) * (result[i][j] - mean);
			double sd = sum / result.Length;
			for (int i = 0; i < result.Length; ++i)
				result[i][j] = (result[i][j] - mean) / sd;
		}
		return result;
	}
	private static int[] InitClustering(int numTuples, int numClusters, int seed)
	{

		int[] clustering = new int[numTuples];
		for (int i = 0; i < numClusters; ++i)
			clustering[i] = i;
		for (int i = numClusters; i < clustering.Length; ++i)
			clustering[i] = Random.Range(0,numClusters);
			
		return clustering;
	}
	private static bool UpdateMeans(double[][] data, int[] clustering, double[][] means)
	{
		int numClusters = means.Length;
		int[] clusterCounts = new int[numClusters];
		for (int i = 0; i < data.Length; ++i)
		{
			int cluster = clustering[i];
			++clusterCounts[cluster];
		}
		
		for (int k = 0; k < numClusters; ++k)
			if (clusterCounts[k] == 0)
				return false;
		
		for (int k = 0; k < means.Length; ++k)
			for (int j = 0; j < means[k].Length; ++j)
				means[k][j] = 0.0;
		
		for (int i = 0; i < data.Length; ++i)
		{
			int cluster = clustering[i];
			for (int j = 0; j < data[i].Length; ++j)
				means[cluster][j] += data[i][j]; // accumulate sum
		}
		
		for (int k = 0; k < means.Length; ++k)
			for (int j = 0; j < means[k].Length; ++j)
				means[k][j] /= clusterCounts[k]; // danger of div by 0
		return true;
	}
	private static double[][] Allocate(int numClusters, int numColumns)
	{
		double[][] result = new double[numClusters][];
		for (int k = 0; k < numClusters; ++k)
			result[k] = new double[numColumns];
		return result;
	}
	private static bool UpdateClustering(double[][] data, int[] clustering, double[][] means)
	{
		int numClusters = means.Length;
		bool changed = false;
		
		int[] newClustering = new int[clustering.Length];
		clustering.CopyTo(newClustering, clustering.Length);


		
		double[] distances = new double[numClusters];
		
		for (int i = 0; i < data.Length; ++i) 
		{
			for (int k = 0; k < numClusters; ++k)
				distances[k] = Distance(data[i], means[k]);
			
			int newClusterID = MinIndex(distances);
			if (newClusterID != newClustering[i])
			{
				changed = true;
				newClustering[i] = newClusterID;
			}
		}
		
		if (changed == false)
			return false;
		
		int[] clusterCounts = new int[numClusters];
		for (int i = 0; i < data.Length; ++i)
		{
			int cluster = newClustering[i];
			++clusterCounts[cluster];
		}
		
		for (int k = 0; k < numClusters; ++k)
			if (clusterCounts[k] == 0)
				return false;

		newClustering.CopyTo(clustering,clustering.Length);

		return true; // no zero-counts and at least one change
	}
	private static double Distance(double[] tuple, double[] mean)
	{
		double sumSquaredDiffs = 0.0;
		for (int j = 0; j < tuple.Length; ++j)
			sumSquaredDiffs += Mathf.Pow((float)(tuple[j] - mean[j]), 2f);
		return Mathf.Sqrt((float)sumSquaredDiffs);
	}
	private static int MinIndex(double[] distances)
	{
		int indexOfMin = 0;
		double smallDist = distances[0];
		for (int k = 0; k < distances.Length; ++k) {
			if (distances[k] < smallDist) {
				smallDist = distances[k];
				indexOfMin = k;
			}
		}
		return indexOfMin;
	}

}
		

