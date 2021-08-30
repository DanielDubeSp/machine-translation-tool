Feature: TranslateService
Simple translation service

@TranslateServiceTag
Scenario: 'Hello world!' to ES is ok
Given the text to translate Hello world!
 And the source language en
 And the target language es
 When the text is translated
 Then the result text should be ¡Hola Mundo!

Scenario: 'Hello world!' to DE is ok
Given the text to translate Hello world!
 And the source language en
 And the target language de
 When the text is translated
 Then the result text should be Hallo Welt!

Scenario: 'Hello world!' to EN is ok
Given the text to translate Hello world!
 And the source language en
 And the target language en
 When the text is translated
 Then the result text should be Hello world!

Scenario: 'Hola mundo!' to EN is ok (capitalized)
Given the text to translate ¡Hola mundo!
 And the source language es
 And the target language en
 When the text is translated
 Then the result text should be Hello World!

Scenario: 'Hola Mundo!' to EN is ok
Given the text to translate ¡Hola Mundo!
 And the source language es
 And the target language en
 When the text is translated
 Then the result text should be Hello World!

Scenario: 'Hello world!' to FR fails (not existing target lang)
Given the text to translate Hello world!
 And the source language en
 And the target language fr
 When the text is translated
 Then a NotAllowedLanguageException exception should be thrown for language fr

Scenario: '¡Hola mundo!' from ESS fails (not existing source lang)
Given the text to translate ¡Hola mundo!
 And the source language ess
 And the target language en
 When the text is translated
 Then a NotAllowedLanguageException exception should be thrown for language ess

 Scenario: Empty string is ok
Given an empty text to translate
 And the source language en
 And the target language es
 When the text is translated
 Then the result text is empty

 #TODO: Add more tests ... 
