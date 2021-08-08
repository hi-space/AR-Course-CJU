using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARFaceMeshVisualizer : MonoBehaviour
{
    private ARFace m_Face { get; set; }

    ParticleSystem m_ParticleSystem;
    ParticleSystem.Particle[] m_Particles;
    int m_NumParticles;

    private void Awake()
    {
        m_Face = GetComponent<ARFace>();
        m_ParticleSystem = GetComponent<ParticleSystem>();
        m_Particles = new ParticleSystem.Particle[m_ParticleSystem.main.maxParticles];
    }

    private void Update()
    {
        // ModelTransform.localPosition = m_Face.vertices[16];
        // point.position = transform.TransformPoint(vertex);

        int numParticles = m_Face.vertices.Length;
        if (m_Particles == null || m_Particles.Length < numParticles)
            m_Particles = new ParticleSystem.Particle[numParticles];

        for (int i = 0; i < numParticles; ++i)
        {
            m_Particles[i].startColor = m_ParticleSystem.main.startColor.color;
            m_Particles[i].startSize = m_ParticleSystem.main.startSize.constant;
            m_Particles[i].position = m_Face.vertices[i];
            m_Particles[i].remainingLifetime = 1f;
        }

        for (int i = numParticles; i < m_NumParticles; ++i)
        {
            m_Particles[i].remainingLifetime = -1f;
        }

        m_ParticleSystem.SetParticles(m_Particles, Mathf.Max(numParticles, m_NumParticles));
        m_NumParticles = numParticles;
    }
}
