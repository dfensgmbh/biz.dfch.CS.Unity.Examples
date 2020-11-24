using System;
using UnityEngine;

public class CubeColor : MonoBehaviour
{
    private Renderer renderer = new Renderer();
    private int maxKelvinTemperature = 400;
    private int minKelvinTemperature = -400;
    private readonly string[] temperatureUnits = { "Celsius", "Fahrenheit", "Kelvin" };

    public double Temperature = 0d;
    public string TemperatureUnit = "Kelvin";
    
    // Start is called before the first frame update
    void Start()
    {
        var resultColor = MapTemperatureToColor(Temperature);

        renderer = GetComponent<Renderer>();
        renderer.material.SetColor("_Color", resultColor);
    }

    private Color MapTemperatureToColor(double temperature)
    {
        Debug.Log($"START Mapping temperature ('{temperature}') to color");

        if (TemperatureUnit != temperatureUnits[2])
        {
            temperature = ConvertToKelvin(temperature);
            Debug.Log($"END Converting temperature ('{temperature}')");
        }

        var gapMinToMaxKelvinTemperature = maxKelvinTemperature - minKelvinTemperature;
        
        Debug.Log($"Distance between max and min temperature value is: '{gapMinToMaxKelvinTemperature}'");

        var valueR = (gapMinToMaxKelvinTemperature - (maxKelvinTemperature - temperature)) / gapMinToMaxKelvinTemperature;
        var valueB = 1 - valueR;

        Debug.Log($"Creating color with R '{valueR}' G '0' and B '{valueB}'");

        var result = new Color((float)valueR, 0, (float)valueB);

        Debug.Log($"END Result Color: '{result}'");

        return result;
    }

    private double ConvertToKelvin(double temperature)
    {
        switch (TemperatureUnit)
        {
            case "Celsius":
                Debug.Log($"START Converting '{temperature}' from Celsius to Kelvin");
                return temperature + 273.15;
            case "Fahrenheit":
                Debug.Log($"START Converting '{temperature}' from Fahrenheit to Kelvin");
                return (temperature - 32) * 5 / 9 + 273.15;
            default:
                Debug.Log($"Temperature has not supported temperature unit: '{TemperatureUnit}'");
                throw new NotSupportedException();
        }
    }
}
