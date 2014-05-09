using UnityEngine;
using System.Collections;

public class CrateringMesh : MonoBehaviour {

	float radius = .1f;
	float pull = .1f;
	private MeshFilter unappliedMesh ;

	void Start(){


}
	enum FallOff { Gauss, Linear, Needle }
	FallOff fallOff = FallOff.Gauss;
	
	static float LinearFalloff (float distance  ,float inRadius  ) {
		return Mathf.Clamp01(1.0f - distance / inRadius);
	}
	
	static float GaussFalloff (float distance  ,float inRadius ) {
		return Mathf.Clamp01 (Mathf.Pow (360.0f, -Mathf.Pow (distance / inRadius, 2.5f) - 0.01f));
	}
	
	public float NeedleFalloff (float dist ,float inRadius)
	{
		return -(dist*dist) / (inRadius * inRadius) + 1.0f;
	}
	
	public MeshFilter DeformMesh (MeshFilter meshF , Vector3 position , float power ,float inRadius)
	{
		unappliedMesh = meshF;
		Mesh mesh = unappliedMesh.mesh;
		Vector3[] vertices = mesh.vertices;
		Vector3[] normals = mesh.normals;
		float sqrRadius = inRadius * inRadius;
		
		// Calculate averaged normal of all surrounding vertices	
		Vector3 averageNormal = Vector3.zero;
		float falloff;
		for (int i=0;i<vertices.Length;i++)
		{
			float sqrMagnitude = (vertices[i] - position).sqrMagnitude;
			// Early out if too far away
			if (sqrMagnitude > sqrRadius)
				continue;
			
			float distance = Mathf.Sqrt(sqrMagnitude);
			falloff = LinearFalloff(distance, inRadius);
			averageNormal += falloff * normals[i];
		}
		averageNormal = averageNormal.normalized;
		
		// Deform vertices along averaged normal
		for (int i=0;i<vertices.Length;i++)
		{
			float sqrMagnitude = (vertices[i] - position).sqrMagnitude;
			// Early out if too far away
			if (sqrMagnitude > sqrRadius)
				continue;
			
			float distance = Mathf.Sqrt(sqrMagnitude);
			switch (fallOff)
			{
			case FallOff.Gauss:
				falloff = GaussFalloff(distance, inRadius);
				break;
			case FallOff.Needle:
				falloff = NeedleFalloff(distance, inRadius);
				break;
			default:
				falloff = LinearFalloff(distance, inRadius);
				break;
			}
			
			vertices[i] -= averageNormal * falloff * power;

		}
		
		mesh.vertices = vertices;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		return unappliedMesh;
	}
	
	void OnCollisionEnter (Collision col) {
		for(int i = 0 ; i < col.contacts.Length; i++){
			if(col.gameObject.GetComponent<BulletScript>()!=null){
				Collider co = col.contacts[i].otherCollider;
				MeshFilter filter  = GetComponent<MeshFilter>();
				Vector3 relativePoint = filter.transform.InverseTransformPoint(collider.ClosestPointOnBounds(co.transform.position));
				filter =  DeformMesh(filter, relativePoint,pull, radius*col.gameObject.GetComponent<BulletScript>().damage);
				ApplyMeshCollider();
				
			}
		}

	}
	
	void ApplyMeshCollider () {
		if (unappliedMesh && unappliedMesh.GetComponent<MeshCollider>()) {
			unappliedMesh.GetComponent<MeshCollider>().mesh = unappliedMesh.mesh;
		}
		unappliedMesh = null;
	}
}
