using System.Collections;
using UnityEngine;

public class JSonParser {
    /// <summary>
    /// Returns the bool safed in the JSon string.
    /// </summary>
    /// <param name="JSonString">The JsonString where the Parser will look for the data</param>
    /// <param name="tag">The tag you want to get.</param>
    public bool getBool(string JSonString, string tag) {      
        JSonString = findTag(JSonString,tag);
        Debug.Log("Der bool is : " + parseBoolValue(JSonString).ToString());
        return parseBoolValue(JSonString);
    }

    /// <summary>
    /// the following regex is implemented : (*)(")(KEY)(") 
    /// </summary>
    /// <param name="JSonString">The JsonString where the Parser will look for the data</param>
    /// <param name="tag">The tag you want to get.</param>
    private string findTag(string JSonString, string tag) {
        char[] jStringAsChar = JSonString.ToCharArray();
        char[] tagArray = tag.ToCharArray();
        for(int i = 0; i < jStringAsChar.Length; i++) {
            Debug.Log("Ist : " + jStringAsChar[i] + " ein \" ?");
            if(jStringAsChar[i] == '\"') {
                Debug.Log(" Checking for tag ...");
                if(checkTag(JSonString.Substring(i+1).ToCharArray(), tagArray)) {
                    Debug.Log("Tag does match, der letzte teil des strings lautet : " + JSonString.Substring(i + 1 + tagArray.Length + 2));
                    return JSonString.Substring(i + 1 + tagArray.Length + 2);

                } else {
                    Debug.Log("Falscher Tag.");
                }
            }
        }
        return null;
    }

    private bool checkTag(char[] s, char[] tag) {
        try {
            bool matches = true;
            int i = 0;
            while (matches) {
                string sss = "";
                string ssss = "";
                foreach(char c in s) {
                    sss = sss + c;
                }
                foreach (char c in tag) {
                    ssss = ssss + c;
                }
                Debug.Log("Die zuprüfenden strings sind: " + sss + " und " + ssss + " .");
                Debug.Log(" ist " + s[i] + " == " + tag[i] + " ?");
                if (s[i] == tag[i]) {
                    Debug.Log( " JA! \n" + "ist " + s[i] + " == " + tag[i] + " ?");
                    if (i == tag.Length - 1) {
                        if (s[i + 1] == '\"') {
                            return true;
                        }
                    } else {
                        i++;
                    }
                } else {
                    return false;
                }
            }
            return false;
        } catch {
            return false;
        }
    }

    private bool parseBoolValue(string jsonString) {
        char[] ar = jsonString.ToCharArray();
        if (ar[0] == 'f') {
            if(ar[1] == 'a') {
                if (ar[2] == 'l') {
                    if(ar[3] == 's') {
                        if (ar[4] == 'e') {
                            return false;
                        } else {
                            throw new NoBoolException("The String does not contain \"true\" or \"false\" in the first word. CAUTION: THE RESULT IS FALSE.");
                        }
                    } else {
                        throw new NoBoolException("The String does not contain \"true\" or \"false\" in the first word. CAUTION: THE RESULT IS FALSE.");
                    }
                } else {
                    throw new NoBoolException("The String does not contain \"true\" or \"false\" in the first word. CAUTION: THE RESULT IS FALSE.");
                }
            } else {
                throw new NoBoolException("The String does not contain \"true\" or \"false\" in the first word. CAUTION: THE RESULT IS FALSE.");
            }
        } else if (ar[0] == 't') {
            if(ar[1] == 'r') {
                if(ar[2] == 'u') {
                    if(ar[3] == 'e') {
                        return true;
                    } else {
                        throw new NoBoolException("The String does not contain \"true\" or \"false\" in the first word. CAUTION: THE RESULT IS FALSE.");
                    }
                } else {
                    throw new NoBoolException("The String does not contain \"true\" or \"false\" in the first word. CAUTION: THE RESULT IS FALSE.");
                }
            } else {
                throw new NoBoolException("The String does not contain \"true\" or \"false\" in the first word. CAUTION: THE RESULT IS FALSE.");
            }
        } else {
            throw new NoBoolException("The String does not contain \"true\" or \"false\" in the first word. CAUTION: THE RESULT IS FALSE.");
        }
    }








}

public class NoBoolException: System.Exception {
    public NoBoolException() {

    }
    public NoBoolException(string message) : base(message) {
        
    }
}