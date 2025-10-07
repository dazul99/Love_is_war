using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ResponseOption
{
    public string Texto;
    public int Like;
    public int Love;
    public int Leave;
    public int Travel;
}


public class AppDialogue
{
    public int ID;
    public string Texto;
    public List<ResponseOption> Respuestas = new List<ResponseOption>();
}

public class TextManager : MonoBehaviour
{
    public TextAsset[] csvFile;

    public Dictionary<int, AppDialogue> dialogueData = new Dictionary<int, AppDialogue>();

    public void LoadAppCSV(int x)
    {
        if (csvFile == null)
        {
            Debug.LogError("CSV file not assigned!");
            return;
        }

        string[] lines = csvFile[x].text.Split('\n');
        if (lines.Length <= 1)
        {
            Debug.LogError("CSV file is empty or malformed!");
            return;
        }

        // Leer cada línea (saltando encabezado)
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] parts = SplitCsvLine(line);
            if (parts.Length < 12)
            {
                Debug.LogWarning($"Invalid line at {i}: {line}");
                continue;
            }

            AppDialogue d = new AppDialogue();
            d.ID = int.Parse(parts[0]);
            d.Texto = parts[1];

            // Respuesta 1
            ResponseOption r1 = new ResponseOption
            {
                Texto = parts[2],
                Like = int.Parse(parts[3]),
                Love = int.Parse(parts[4]),
                Leave = int.Parse(parts[5]),
                Travel = int.Parse(parts[6])
            };

            // Respuesta 2
            ResponseOption r2 = new ResponseOption
            {
                Texto = parts[7],
                Like = int.Parse(parts[8]),
                Love = int.Parse(parts[9]),
                Leave = int.Parse(parts[10]),
                Travel = int.Parse(parts[11])
            };

            d.Respuestas.Add(r1);
            d.Respuestas.Add(r2);

            dialogueData[d.ID] = d;
        }

    }

    // Maneja comas dentro de comillas
    private string[] SplitCsvLine(string line)
    {
        List<string> result = new List<string>();
        bool inQuotes = false;
        string current = "";

        foreach (char c in line)
        {
            if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(current);
                current = "";
            }
            else
            {
                current += c;
            }
        }

        result.Add(current);
        return result.ToArray();
    }


    public string[] GetText(int iD)
    {

        string[] texto = new string[3]; 
        texto[0] = dialogueData[iD].Texto.ToString();
        texto[1] = dialogueData[iD].Respuestas[0].Texto.ToString();
        texto[2] = dialogueData[iD].Respuestas[1].Texto.ToString();

        return texto;

    }

    public int[] GetParameters(bool option, int iD)
    {
        int i = 0;
        if (option) { i = 1; } 
        int[] parametros = new int[4];

        parametros[0] = dialogueData[iD].Respuestas[i].Like;
        parametros[1] = dialogueData[iD].Respuestas[i].Love;
        parametros[2] = dialogueData[iD].Respuestas[i].Leave;
        parametros[3] = dialogueData[iD].Respuestas[i].Travel;

        return parametros;
    }
}
