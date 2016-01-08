﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Page : IPage {

    string text;
    List<Character> leftCharacters;
    List<Character> rightCharacters;
    List<Process> actions;

    Process onFirstEnterProcess;
    Process onEnterProcess;
    Process onFirstExitProcess;
    Process onExitProcess;

    PageType pageType;

    bool hasEnteredBefore;
    bool hasExitedBefore;

    public static int idCount = 0;
    int id;

    public Page(string text = "", PageType pageType = PageType.NORMAL, List<Character> left = null, List<Character> right = null,
        Process onFirstEnter = null, Process onEnter = null, Process onFirstExit = null, Process onExit = null,
        List<Process> actions = null) {
        this.text = text;
        this.actions = (actions != null) ? actions : new List<Process>();
        if (left != null) setLeft(left); else this.leftCharacters = new List<Character>();
        if (right != null) setRight(right); else this.rightCharacters = new List<Character>();
        this.onFirstEnterProcess = onFirstEnter;
        this.onEnterProcess = onEnter;
        this.onFirstExitProcess = onFirstExit;
        this.onExitProcess = onExit;

        this.pageType = pageType;
    }

    public void setText(string text) {
        this.text = text;
    }

    public void setPageType(PageType pageType) {
        this.pageType = pageType;
    }

    public PageType getPageType() {
        return pageType;
    }

    public void onEnter() {
        if (!hasEnteredBefore && onFirstEnterProcess != null) {
            hasEnteredBefore = true;
            onFirstEnter();
        } else if (onEnterProcess != null) {
            onEnterProcess.play();
        }
    }

    public void onFirstEnter() {
        onFirstEnterProcess.play();
    }

    public void onExit() {
        if (!hasExitedBefore && onFirstExitProcess != null) {
            hasExitedBefore = true;
            onFirstExit();
        } else if (onExitProcess != null) {
            onExitProcess.play();
        }
    }

    public void onFirstExit() {
        onFirstExitProcess.play();
    }

    public string getText() {
        return text;
    }

    public List<Process> getActions() {
        return actions;
    }

    public void setOnEnter(Process p) {
        onEnterProcess = p;
    }

    public void setOnFirstEnter(Process p) {
        onFirstExitProcess = p;
    }

    public void setOnExit(Process p) {
        onExitProcess = p;
    }

    public void setOnFirstExit(Process p) {
        onFirstExitProcess = p;
    }

    public List<Character> getCharacters(bool isRightSide) {
        return isRightSide ? rightCharacters : leftCharacters;
    }

    public void setAction(Process action, int index) {
        if (0 <= index && index < ActionGridManager.ROWS * ActionGridManager.COLS) {
            actions[index] = action;
        } else {
            throw new IndexOutOfRangeException("Bad input. Index: " + index);
        }
    }

    public void clearAction(int index) {
        if (0 <= index && index < ActionGridManager.ROWS * ActionGridManager.COLS) {
            actions[index] = null;
        } else {
            throw new IndexOutOfRangeException("Bad input. Index: " + index);
        }
    }

    public void setActions(List<Process> actions) {
        if (actions.Count > ActionGridManager.ROWS * ActionGridManager.COLS) {
            throw new ArgumentException("Size of params is: " + actions.Count + ", which is greater than " + ActionGridManager.ROWS * ActionGridManager.COLS);
        }
        this.actions = actions;
    }

    public void setLeft(List<Character> characters) {
        setSide(characters, false);
    }

    public void setRight(List<Character> characters) {
        setSide(characters, true);
    }

    public void setSide(List<Character> characters, bool side) {
        repeatedCharacterCheck(characters);
        if (!side) {
            leftCharacters = characters;
        } else {
            rightCharacters = characters;
        }
    }

    /**
     * Check if repeated characters exist on a side, if so, append A->Z for each repeated character
     * For example: Steve A, Steve B
     */
    public void repeatedCharacterCheck(List<Character> characters) {
        Dictionary<string, List<Character>> repeatedCharacters = new Dictionary<string, List<Character>>();
        foreach (Character c in characters) {
            if (!repeatedCharacters.ContainsKey(c.getName())) {
                List<Character> characterList = new List<Character>();
                characterList.Add(c);
                repeatedCharacters.Add(c.getName(), characterList);
            } else {
                repeatedCharacters[c.getName()].Add(c);
            }
        }

        foreach (KeyValuePair<string, List<Character>> cPair in repeatedCharacters) {
            if (cPair.Value.Count > 1) {
                for (int i = 0; i < cPair.Value.Count; i++) {
                    Character c = cPair.Value[i];
                    c.setName(string.Format("{0} {1}", c.getName(), Util.intToLetter(i + 1)));
                }
            }
        }
    }

    public int getId() {
        return idCount;
    }

    public override bool Equals(object obj) {
        // If parameter is null return false.
        if (obj == null) {
            return false;
        }

        // If parameter cannot be cast to Page return false.
        Page p = obj as Page;
        if ((object) p == null) {
            return false;
        }

        // Return true if the fields match:
        return p.getId() == this.getId();
    }

    public override int GetHashCode() {
        return id;
    }
}