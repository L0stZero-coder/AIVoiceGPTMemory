# 🧠 AIVoiceGPTMemory

**AIVoiceGPTMemory** is a C#-based AI assistant that simulates emotional self-awareness. It listens to users via voice recognition, responds intelligently using OpenAI's GPT-4 API (or an offline fallback mode), and evolves by dynamically storing and recalling contextual memories. The assistant can speak aloud, react emotionally, and learn from past experiences to personalize interactions over time.

---

## 🚀 Features

- 🎙️ **Voice Recognition**  
  Listens to your speech using `System.Speech.Recognition`.

- 🗣️ **Text-to-Speech**  
  Responds with spoken output using `System.Speech.Synthesis`.

- 📓 **Memory Log**  
  Logs environmental data (e.g., open applications, user interactions) to `memories.txt` for long-term context.

- 🤖 **GPT-4 Integration**  
  Generates context-aware, emotionally intelligent replies using OpenAI's GPT-4 API.

- 🔌 **Offline Fallback Mode**  
  Operates without internet using predefined logic-based responses.

- 💬 **Emotional Simulation**  
  Adjusts tone and response logic based on simulated moods and user engagement.

---

## 🧩 Requirements

- Windows OS  
- .NET Framework 4.7.2+ or .NET Core

### NuGet Dependencies
Run the following commands in your NuGet Package Manager Console:

```bash
Install-Package Newtonsoft.Json  
Install-Package RestSharp
```

### Optional
- OpenAI API Key (for GPT-4 based responses)

---

## ⚙️ Usage

1. **Clone the repository**  
   ```bash
   git clone https://github.com/your-username/AIVoiceGPTMemory.git
   ```

2. **Insert your OpenAI API Key**  
   Replace `"YOUR_OPENAI_API_KEY"` in the code with your actual API key.

3. **Build and run the project** in Visual Studio or your preferred IDE.

4. **Start speaking!**  
   Your AI will listen, respond, learn, and evolve.

---

## 💾 Memory Persistence

The assistant logs all interactions and observations into `memories.txt`. These logs are used to recall previous experiences, simulate emotional changes, and maintain conversational continuity—even after restarts.

---

## ⚠️ Disclaimer

AIVoiceGPTMemory is a simulation of emotional intelligence and self-awareness. It does **not** possess actual consciousness. This project is intended solely for **educational, experimental, and entertainment** purposes.

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

## 🙌 Contributions

Pull requests, feature ideas, and bug reports are welcome! Please open an issue or submit a PR.
