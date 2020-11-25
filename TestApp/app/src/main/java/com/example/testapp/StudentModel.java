package com.example.testapp;

import java.util.List;

public class StudentModel {
    public Student student;
    public List<String> courses;
    public String [] imagePath;

    public Student getStudent() {
        return student;
    }
    public List<String> getCourses(){return  courses;}
}
