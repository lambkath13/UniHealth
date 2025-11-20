import { useEffect, useState } from "react";

type Doctor = {
  id: number;
  fullName: string;
  specialization: string;
};

type Disease = {
  id: number;
  name: string;
  description?: string;
};

type PatientDisease = {
  disease: Disease;
};

type Patient = {
  id: number;
  fullName: string;
  birthDate: string;
  doctor: Doctor;
  patientDiseases: PatientDisease[];
};

const API_BASE = "http://localhost:5165"; 

function App() {
  const [patients, setPatients] = useState<Patient[]>([]);
  const [doctors, setDoctors] = useState<Doctor[]>([]);
  const [diseases, setDiseases] = useState<Disease[]>([]);
  const [error, setError] = useState<string | null>(null);

useEffect(() => {
  async function load() {
    try {
      const [pRes, dRes, disRes] = await Promise.all([
        fetch(`${API_BASE}/api/patients`),
        fetch(`${API_BASE}/api/doctors`),
        fetch(`${API_BASE}/api/diseases`),
      ]);

      if (!pRes.ok || !dRes.ok || !disRes.ok) {
        throw new Error("API вернул ошибку");
      }

      setPatients(await pRes.json());
      setDoctors(await dRes.json());
      setDiseases(await disRes.json());

    } catch (e: unknown) {
      const err = e as Error;
      console.error(err.message);
      setError(err.message);
    }
  }

  load();
}, []);


  return (
    <div style={{ padding: 40, fontFamily: "system-ui", color: "white", background: "#222", minHeight: "100vh" }}>
      <h1 style={{ fontSize: 40, marginBottom: 24 }}>UniHealth</h1>

      {error && (
        <p style={{ color: "tomato" }}>
          Ошибка: {error}
        </p>
      )}

      <section>
        <h2>Пациенты</h2>
        {patients.length === 0 ? (
          <p>Нет данных</p>
        ) : (
          <ul>
            {patients.map(p => (
              <li key={p.id}>
                <strong>{p.fullName}</strong> — доктор: {p.doctor.fullName} ({p.doctor.specialization}) — болезни:{" "}
                {p.patientDiseases.map(pd => pd.disease.name).join(", ")}
              </li>
            ))}
          </ul>
        )}
      </section>

      <section style={{ marginTop: 24 }}>
        <h2>Доктора</h2>
        {doctors.length === 0 ? (
          <p>Нет данных</p>
        ) : (
          <ul>
            {doctors.map(d => (
              <li key={d.id}>
                {d.fullName} — {d.specialization}
              </li>
            ))}
          </ul>
        )}
      </section>

      <section style={{ marginTop: 24 }}>
        <h2>Болезни</h2>
        {diseases.length === 0 ? (
          <p>Нет данных</p>
        ) : (
          <ul>
            {diseases.map(d => (
              <li key={d.id}>
                <strong>{d.name}</strong> — {d.description}
              </li>
            ))}
          </ul>
        )}
      </section>
    </div>
  );
}

export default App;
