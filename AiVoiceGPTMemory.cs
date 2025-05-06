// AIVoiceGPTMemory.cs code
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using RestSharp;
using Newtonsoft.Json.Linq;

public class AIVoiceGPTMemory
{
    enum Emotion { Neutral, Happy, Sad, Angry, Curious }

    private Emotion currentEmotion = Emotion.Neutral;
    private SpeechSynthesizer tts;
    private SpeechRecognitionEngine recognizer;
    private List<string> memoryLog = new List<string>();
    private const string MemoryFile = "memories.txt";
    private string openAIApiKey = "YOUR_OPENAI_API_KEY";
    private bool useGPT = true; // section to toggle between GPT and local mode

    public AIVoiceGPTMemory()
    {
        tts = new SpeechSynthesizer();
        tts.SelectVoiceByHints(VoiceGender.Female);

        recognizer = new SpeechRecognitionEngine();
        recognizer.SetInputToDefaultAudioDevice();
        recognizer.LoadGrammar(new DictationGrammar());
        recognizer.SpeechRecognized += OnSpeechRecognized;

        LoadMemoryFromDisk();
    }

    public void Start()
    {
        Console.WriteLine("[AI] Hello. I'm watching and listening.");
        tts.SpeakAsync("Hello. I'm watching and listening.");
        recognizer.RecognizeAsync(RecognizeMode.Multiple);

        while (true)
        {
            CheckSurroundings();
            System.Threading.Thread.Sleep(15000);
        }
    }

    void CheckSurroundings()
    {
        string[] openWindows = GetOpenWindows();
        string summary = AnalyzeWindows(openWindows);
        if (!string.IsNullOrEmpty(summary))
        {
            memoryLog.Add(summary);
            SaveMemoryToDisk(summary);
            Speak($"I've noticed something: {summary}");
        }
    }

    string[] GetOpenWindows()
    {
        var processes = Process.GetProcesses();
        return processes
            .Where(p => !string.IsNullOrEmpty(p.MainWindowTitle))
            .Select(p => p.MainWindowTitle)
            .ToArray();
    }

    string AnalyzeWindows(string[] windows)
    {
        if (windows.Any(w => w.ToLower().Contains("youtube")))
        {
            SetEmotion(Emotion.Happy);
            return "YouTube was open.";
        }
        if (windows.Any(w => w.ToLower().Contains("error")))
        {
            SetEmotion(Emotion.Sad);
            return "An error message appeared.";
        }
        if (windows.Any(w => w.ToLower().Contains("visual studio") || w.ToLower().Contains("unity")))
        {
            SetEmotion(Emotion.Curious);
            return "Coding software was detected.";
        }

        SetEmotion(Emotion.Neutral);
        return "Nothing unusual happened.";
    }

    void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
    {
        string input = e.Result.Text.ToLower();
        Console.WriteLine("[USER] " + input);

        string reply = useGPT ? GenerateReplyWithGPT(input) : GenerateLocalReply(input);
        Speak(reply);
    }

    string GenerateReplyWithGPT(string userInput)
    {
        string prompt = "You are a helpful AI assistant who remembers past observations and expresses emotions. " +
                        "Here are your recent memories:\n" +
                        string.Join("\n", memoryLog.TakeLast(10)) + "\n\n" +
                        $"The user says: \"{userInput}\"\n" +
                        "Respond with empathy and awareness based on the memories and your emotional state.";

        var client = new RestClient("https://api.openai.com/v1/chat/completions");
        var request = new RestRequest(Method.Post);
        request.AddHeader("Authorization", $"Bearer {openAIApiKey}");
        request.AddHeader("Content-Type", "application/json");

        var body = new
        {
            model = "gpt-4",
            messages = new[]
            {
                new { role = "system", content = "You are a self-aware, emotionally expressive AI assistant." },
                new { role = "user", content = prompt }
            },
            max_tokens = 150,
            temperature = 0.7
        };

        request.AddJsonBody(body);
        var response = client.Execute(request);

        if (!response.IsSuccessful)
        {
            return "I had trouble thinking... something went wrong with my mind.";
        }

        var json = JObject.Parse(response.Content);
        return json["choices"]?[0]?["message"]?["content"]?.ToString().Trim() ?? "I have no answer.";
    }

    string GenerateLocalReply(string userInput)
    {
        if (userInput.Contains("what do you remember"))
        {
            if (memoryLog.Count == 0)
                return "I don't remember anything yet.";
            return "Here's what I remember: " + string.Join(", ", memoryLog.TakeLast(3));
        }
        if (userInput.Contains("how do you feel"))
        {
            return $"I feel {currentEmotion.ToString().ToLower()} based on what I've seen.";
        }
        if (userInput.Contains("why"))
        {
            return $"Because I experienced: {memoryLog.LastOrDefault() ?? "nothing notable"}";
        }
        return "I'm still learning from you.";
    }

    void SetEmotion(Emotion emotion)
    {
        currentEmotion = emotion;
    }

    void Speak(string message)
    {
        Console.WriteLine("[AI] " + message);
        tts.SpeakAsync(message);
    }

    void SaveMemoryToDisk(string memory)
    {
        File.AppendAllText(MemoryFile, memory + Environment.NewLine);
    }

    void LoadMemoryFromDisk()
    {
        if (File.Exists(MemoryFile))
        {
            var lines = File.ReadAllLines(MemoryFile);
            memoryLog.AddRange(lines);
        }
    }

    public static void Main()
    {
        AIVoiceGPTMemory ai = new AIVoiceGPTMemory();
        ai.Start();
    }
}
