using Itmo.ObjectOrientedProgramming.Lab2.Courses;
using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.Repository;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.Users;
using Itmo.ObjectOrientedProgramming.Lab2.ValueTypes;
using System.Collections.ObjectModel;
using Xunit;

namespace Lab2.Tests;

public class AllCourseBuilderTests
{
    public class LabworkTests
    {
        [Fact]
        public void Labwork_SetByAuthorTest_ChangesValue()
        {
            var testUser1 = new BasicUser("1");
            var testUser2 = new BasicUser("2");

            var basicLabwork = new BasicLabwork(testUser1.Id, "lab1", "desc1", new Points(100), "rc1", null);

            basicLabwork.RatingCriteria.SetValue(testUser1.Id, "new value");

            Assert.Equal("new value", basicLabwork.RatingCriteria.GetValue(testUser1.Id));
        }

        [Fact]
        public void Labwork_SetByStrangerTest_ThrowsException()
        {
            var testUser1 = new BasicUser("1");
            var testUser2 = new BasicUser("2");

            var basicLabwork = new BasicLabwork(testUser1.Id, "lab1", "desc1", new Points(100), "rc1", null);

            Assert.ThrowsAny<Exception>(() => basicLabwork.RatingCriteria.SetValue(testUser2.Id, "smth evil"));
            Assert.Equal("rc1", basicLabwork.RatingCriteria.GetValue(testUser2.Id));
        }

        [Fact]
        public void Labwork_DeepCopyCloneTest_CopyIsDeep()
        {
            var testUser1 = new BasicUser("1");
            var testUser2 = new BasicUser("2");

            var basicLabwork = new BasicLabwork(testUser1.Id, "lab1", "desc1", new Points(100), "rc1", null);

            Labwork basicLabwork2 = basicLabwork.Clone();

            Assert.Equal("lab1", basicLabwork2.Name.GetValue(testUser1.Id));
            Assert.NotEqual(basicLabwork.Id, basicLabwork2.Id);
            Assert.Equal(basicLabwork.Id, basicLabwork2.SourceLabworkId);

            basicLabwork.Name.SetValue(testUser1.Id, "new value");

            Assert.Equal("new value", basicLabwork.Name.GetValue(testUser1.Id));
            Assert.Equal("lab1", basicLabwork2.Name.GetValue(testUser1.Id));
        }

        [Fact]
        public void Labwork_BuilderTest_BuildsSuccessfully()
        {
            var labworkBuilder = new BasicLabworkBuilder();

            var testUser1 = new BasicUser("1");
            const string description = "bububu";
            const string labName = "lab1";
            var points = new Points(50);
            const string ratingCriteria = "aaa";

            var labworkBuilder2 = new BasicLabworkBuilder();

            Labwork buildedLab = labworkBuilder2.WithAuthor(testUser1.Id)
                .WithDescription(description)
                .WithName(labName)
                .WithPoints(points)
                .WithRatingCriteria(ratingCriteria)
                .BuildLabwork();

            Assert.Equal("bububu", buildedLab.Description.GetValue(testUser1.Id));
            Assert.Equal(labName, buildedLab.Name.GetValue(testUser1.Id));
            Assert.Equal(testUser1.Id, buildedLab.AuthorId);
        }
    }

    public class LectureTests
    {
        [Fact]
        public void Lecture_SetByAuthorTest_ChangesValue()
        {
            var testUser1 = new BasicUser("1");
            var testUser2 = new BasicUser("2");

            var basicLecture = new BasicLecture(testUser1.Id, "lec1", "desc1", "content1", null);

            basicLecture.Content.SetValue(testUser1.Id, "new value");

            Assert.Equal("new value", basicLecture.Content.GetValue(testUser1.Id));
        }

        [Fact]
        public void Lecture_SetByStrangerTest_ThrowsException()
        {
            var testUser1 = new BasicUser("1");
            var testUser2 = new BasicUser("2");

            var basicLecture = new BasicLecture(testUser1.Id, "lec1", "desc1", "content1", null);

            Assert.ThrowsAny<Exception>(() => basicLecture.Content.SetValue(testUser2.Id, "smth evil"));
            Assert.Equal("content1", basicLecture.Content.GetValue(testUser2.Id));
        }

        [Fact]
        public void Lecture_DeepCopyCloneTest_CopyIsDeep()
        {
            var testUser1 = new BasicUser("1");
            var testUser2 = new BasicUser("2");

            var basicLecture = new BasicLecture(testUser1.Id, "lec1", "desc1", "content1", null);

            Lecture basicLecture2 = basicLecture.Clone();

            Assert.Equal("lec1", basicLecture2.Name.GetValue(testUser1.Id));
            Assert.NotEqual(basicLecture.Id, basicLecture2.Id);
            Assert.Equal(basicLecture.Id, basicLecture2.SourceId);

            basicLecture.Name.SetValue(testUser1.Id, "new value");

            Assert.Equal("new value", basicLecture.Name.GetValue(testUser1.Id));
            Assert.Equal("lec1", basicLecture2.Name.GetValue(testUser1.Id));
        }

        [Fact]
        public void Lecture_BuilderTest_BuildsSuccessfully()
        {
            var lectureBuilder = new BasicLectureBuilder();

            var testUser1 = new BasicUser("1");
            var testUser2 = new BasicUser("2");

            Lecture buildedLab = lectureBuilder.WithAuthorId(testUser1.Id)
                .WithName(testUser1.Name + " lec")
                .WithDescription("bububu")
                .WithContent("content").BuildLecture();

            Assert.Equal("bububu", buildedLab.Description.GetValue(testUser1.Id));
            Assert.Equal(testUser1.Name + " lec", buildedLab.Name.GetValue(testUser1.Id));
            Assert.Equal(testUser1.Id, buildedLab.AuthorId);
        }
    }

    public class SubjectTests
    {
        public class ExamSubjectTests
        {
            [Fact]
            public void ExamSubject_InvalidPointsTest_ThrowsException()
            {
                var subjectBuilder = new ExamSubjectBuilder();

                var examPoints = new Points(100);

                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                var basicLabwork = new BasicLabwork(testUser1.Id, "name1", "desc1", new Points(77), "crt1", null);
                var basicLabwork2 = new BasicLabwork(testUser1.Id, "name2", "desc2", new Points(33), "crt2", null);

                var labworksCollection = new Collection<Labwork>();
                labworksCollection.Add(basicLabwork);
                labworksCollection.Add(basicLabwork2);

                Assert.ThrowsAny<Exception>(() => subjectBuilder.WithName("name")
                    .WithAuthorId(testUser1.Id)
                    .WithExamPoints(examPoints)
                    .WithLabworksStorage(labworksCollection)
                    .BuildSubject());
            }

            [Fact]
            public void ExamSubject_SetByAuthorTest_ChangesValue()
            {
                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                var basicLabwork = new BasicLabwork(testUser1.Id, "lab1", "desc1", new Points(77), "crt1", null);
                var labworksStorage = new List<Labwork>();
                labworksStorage.Add(basicLabwork);
                var examSubject = new ExamSubject(testUser1.Id, "exsubj1", new Points(20), labworksStorage, null);

                examSubject.Name.SetValue(testUser1.Id, "new value");

                Assert.Equal("new value", examSubject.Name.GetValue(testUser1.Id));
            }

            [Fact]
            public void ExamSubject_SetByStrangerTest_ThrowsException()
            {
                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                var basicLabwork = new BasicLabwork(testUser1.Id, "lab1", "desc1", new Points(77), "crt1", null);
                var labworksStorage = new List<Labwork>();
                labworksStorage.Add(basicLabwork);
                var examSubject = new ExamSubject(testUser1.Id, "exsubj1", new Points(20), labworksStorage, null);

                Assert.ThrowsAny<Exception>(() => examSubject.Name.SetValue(testUser2.Id, "smth evil"));
                Assert.Equal("exsubj1", examSubject.Name.GetValue(testUser2.Id));
            }

            [Fact]
            public void ExamSubject_DeepCopyCloneTest_CopyIsDeep()
            {
                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                var basicLabwork = new BasicLabwork(testUser1.Id, "lab1", "desc1", new Points(77), "crt1", null);
                var labworksStorage = new List<Labwork>();
                labworksStorage.Add(basicLabwork);
                var examSubject = new ExamSubject(testUser1.Id, "exsubj1", new Points(20), labworksStorage, null);

                var examSubject2 = examSubject.Clone() as ExamSubject;
                Assert.NotNull(examSubject2);
                Assert.IsType<ExamSubject>(examSubject2);

                Assert.Equal("exsubj1", examSubject2.Name.GetValue(testUser1.Id));
                Assert.NotEqual(examSubject.Id, examSubject2.Id);
                Assert.Equal(examSubject.Id, examSubject2.SourceId);

                examSubject.Name.SetValue(testUser1.Id, "new value");

                Assert.Equal("new value", examSubject.Name.GetValue(testUser1.Id));
                Assert.Equal("exsubj1", examSubject2.Name.GetValue(testUser1.Id));

                labworksStorage[0].Name.SetValue(testUser1.Id, "new value");

                Assert.Equal("new value", labworksStorage[0].Name.GetValue(testUser1.Id));
                Assert.NotEqual("new value", examSubject.LabworksStorage[0].Name.GetValue(testUser1.Id));

                examSubject.LabworksStorage[0].Name.SetValue(testUser1.Id, "new new value");
                Assert.NotEqual("new new value", examSubject2.LabworksStorage[0].Name.GetValue(testUser1.Id));
            }

            [Fact]
            public void ExamSubject_BuildTest_BuildsSuccessfully()
            {
                var subjectBuilder = new ExamSubjectBuilder();

                var examPoints = new Points(20);

                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                var basicLabwork = new BasicLabwork(testUser1.Id, "name1", "desc1", new Points(60), "crt1", null);
                var basicLabwork2 = new BasicLabwork(testUser1.Id, "name2", "desc2", new Points(20), "crt2", null);

                var labworkStorage = new Collection<Labwork>();
                labworkStorage.Add(basicLabwork);
                labworkStorage.Add(basicLabwork2);

                Subject newSubject = subjectBuilder.WithExamPoints(examPoints)
                    .WithAuthorId(testUser1.Id)
                    .WithName("name").WithLabworksStorage(labworkStorage).BuildSubject();

                Assert.Equal(testUser1.Id, newSubject.AuthorId);
                Assert.Equal("name", newSubject.Name.GetValue(testUser2.Id));
            }

            [Fact]
            public void ExamSubject_BuildCloneTest_ClonesSuccessfully()
            {
                var subjectBuilder = new ExamSubjectBuilder();

                var examPoints = new Points(20);

                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                var basicLabwork = new BasicLabwork(testUser1.Id, "name1", "desc1", new Points(60), "crt1", null);
                var basicLabwork2 = new BasicLabwork(testUser1.Id, "name2", "desc2", new Points(20), "crt2", null);

                var labworkStorage = new Collection<Labwork>();
                labworkStorage.Add(basicLabwork);
                labworkStorage.Add(basicLabwork2);

                Subject newSubject = subjectBuilder.WithExamPoints(examPoints)
                    .WithAuthorId(testUser1.Id)
                    .WithLabworksStorage(labworkStorage)
                    .WithName("name").BuildSubject();

                Assert.Equal(testUser1.Id, newSubject.AuthorId);
                Assert.Equal("name", newSubject.Name.GetValue(testUser2.Id));

                Subject newSubject2 = newSubject.Clone();

                Assert.Equal(newSubject.Id, newSubject2.SourceId);
                Assert.Equal(newSubject.Name.GetValue(testUser1.Id), newSubject2.Name.GetValue(testUser2.Id));

                newSubject.Name.SetValue(testUser1.Id, "new name");

                Assert.NotEqual(newSubject.Name.GetValue(testUser1.Id), newSubject2.Name.GetValue(testUser2.Id));
            }
        }

        public class ZachotSubjectTests
        {
            [Fact]
            public void ZachotSubject_InvalidPointsTest_ThrowsException()
            {
                var subjectBuilder = new ZachotSubjectBuilder();

                var zachotPoints = new Points(100);

                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                var basicLabwork = new BasicLabwork(testUser1.Id, "name1", "desc1", new Points(77), "crt1", null);
                var basicLabwork2 = new BasicLabwork(testUser1.Id, "name2", "desc2", new Points(33), "crt2", null);

                var labworkStorage = new Collection<Labwork>();
                labworkStorage.Add(basicLabwork);
                labworkStorage.Add(basicLabwork2);

                Assert.ThrowsAny<Exception>(() => subjectBuilder.WithZachotPoints(zachotPoints)
                    .WithLabworksStorage(labworkStorage)
                    .WithAuthorId(testUser1.Id).BuildSubject());
            }

            [Fact]
            public void ZachotSubject_SetByAuthorTest_ChangesValue()
            {
                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                var basicLabwork = new BasicLabwork(testUser1.Id, "lab1", "desc1", new Points(77), "crt1", null);
                var labworksStorage = new List<Labwork>();
                labworksStorage.Add(basicLabwork);
                var zachotSubject = new ZachotSubject(testUser1.Id, "exsubj1", new Points(20), labworksStorage, null);

                zachotSubject.Name.SetValue(testUser1.Id, "new value");

                Assert.Equal("new value", zachotSubject.Name.GetValue(testUser1.Id));
            }

            [Fact]
            public void ZachotSubject_SetByStrangerTest_ThrowsException()
            {
                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                var basicLabwork = new BasicLabwork(testUser1.Id, "lab1", "desc1", new Points(77), "crt1", null);
                var labworksStorage = new List<Labwork>();
                labworksStorage.Add(basicLabwork);
                var zachotSubject = new ZachotSubject(testUser1.Id, "exsubj1", new Points(20), labworksStorage, null);

                Assert.ThrowsAny<Exception>(() => zachotSubject.Name.SetValue(testUser2.Id, "smth evil"));
                Assert.Equal("exsubj1", zachotSubject.Name.GetValue(testUser2.Id));
            }

            [Fact]
            public void ZachotSubject_DeepCopyCloneTest_CopyIsDeep()
            {
                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                var basicLabwork = new BasicLabwork(testUser1.Id, "lab1", "desc1", new Points(77), "crt1", null);
                var labworksStorage = new List<Labwork>();
                labworksStorage.Add(basicLabwork);
                var zachotSubject = new ZachotSubject(testUser1.Id, "exsubj1", new Points(20), labworksStorage, null);

                var zachotSubject2 = zachotSubject.Clone() as ZachotSubject;
                Assert.NotNull(zachotSubject2);
                Assert.IsType<ZachotSubject>(zachotSubject2);

                Assert.Equal("exsubj1", zachotSubject2.Name.GetValue(testUser1.Id));
                Assert.NotEqual(zachotSubject.Id, zachotSubject2.Id);
                Assert.Equal(zachotSubject.Id, zachotSubject2.SourceId);

                zachotSubject.Name.SetValue(testUser1.Id, "new value");

                Assert.Equal("new value", zachotSubject.Name.GetValue(testUser1.Id));
                Assert.Equal("exsubj1", zachotSubject2.Name.GetValue(testUser1.Id));

                labworksStorage[0].Name.SetValue(testUser1.Id, "new value");

                Assert.Equal("new value", labworksStorage[0].Name.GetValue(testUser1.Id));
                Assert.NotEqual("new value", zachotSubject.LabworksStorage[0].Name.GetValue(testUser1.Id));

                zachotSubject.LabworksStorage[0].Name.SetValue(testUser1.Id, "new new value");
                Assert.NotEqual("new new value", zachotSubject2.LabworksStorage[0].Name.GetValue(testUser1.Id));
            }

            [Fact]
            public void ZachotSubject_BuildTest_BuildsSuccessfully()
            {
                var subjectBuilder = new ZachotSubjectBuilder();

                var zachotPoints = new Points(60);

                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                var basicLabwork = new BasicLabwork(testUser1.Id, "name1", "desc1", new Points(70), "crt1", null);
                var basicLabwork2 = new BasicLabwork(testUser1.Id, "name2", "desc2", new Points(30), "crt2", null);

                var labworkStorage = new Collection<Labwork>();
                labworkStorage.Add(basicLabwork);
                labworkStorage.Add(basicLabwork2);

                Subject newSubject = subjectBuilder.WithZachotPoints(zachotPoints)
                    .WithAuthorId(testUser1.Id)
                    .WithName("name")
                    .WithLabworksStorage(labworkStorage).BuildSubject();

                Assert.Equal(testUser1.Id, newSubject.AuthorId);
                Assert.Equal("name", newSubject.Name.GetValue(testUser2.Id));
            }

            [Fact]
            public void ZachotSubject_BuildCloneTest_ClonesSuccessfully()
            {
                var zachotSubjectBuilder = new ZachotSubjectBuilder();

                var zachotPoints = new Points(60);

                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                var basicLabwork = new BasicLabwork(testUser1.Id, "name1", "desc1", new Points(70), "crt1", null);
                var basicLabwork2 = new BasicLabwork(testUser1.Id, "name2", "desc2", new Points(30), "crt2", null);

                var labworkStorage = new Collection<Labwork>();
                labworkStorage.Add(basicLabwork);
                labworkStorage.Add(basicLabwork2);

                Subject newSubject = zachotSubjectBuilder.WithZachotPoints(zachotPoints)
                    .WithAuthorId(testUser1.Id)
                    .WithName("name").WithLabworksStorage(labworkStorage).BuildSubject();

                Assert.Equal(testUser1.Id, newSubject.AuthorId);
                Assert.Equal("name", newSubject.Name.GetValue(testUser2.Id));

                Subject newSubject2 = newSubject.Clone();

                Assert.Equal(newSubject.Id, newSubject2.SourceId);
                Assert.Equal(newSubject.Name.GetValue(testUser1.Id), newSubject2.Name.GetValue(testUser2.Id));

                newSubject.Name.SetValue(testUser1.Id, "new name");

                Assert.NotEqual(newSubject.Name.GetValue(testUser1.Id), newSubject2.Name.GetValue(testUser2.Id));
            }
        }
    }

    public class RepositoryTests
    {
        [Fact]
        public void UserRepository_AllFunctionsTest_AddsFindsRemovesCorrespondingly()
        {
            var userRepository = new BasicRepository<User>();

            var user1 = new BasicUser("1");
            var user2 = new BasicUser("2");
            var user3 = new BasicUser("3");

            userRepository.Add(user1);

            Assert.Equal(user1, userRepository.Get(user1.Id));

            userRepository.Add(user2);
            userRepository.Add(user3);

            Assert.Equal(user1, userRepository.Get(user1.Id));
            Assert.Equal(user2, userRepository.Get(user2.Id));
            Assert.Equal(user3, userRepository.Get(user3.Id));

            Assert.Null(userRepository.Get(Guid.NewGuid()));

            Assert.True(userRepository.Remove(user2.Id));

            Assert.Equal(user1, userRepository.Get(user1.Id));
            Assert.Null(userRepository.Get(user2.Id));
            Assert.Equal(user3, userRepository.Get(user3.Id));
        }

        [Fact]
        public void SubjectRepository_AllFunctionsTest_AddsFindsRemovesCorrespondingly()
        {
            var subjectRepository = new BasicRepository<Subject>();

            var user1 = new BasicUser("1");
            var user2 = new BasicUser("2");
            var user3 = new BasicUser("3");

            var basicLabwork = new BasicLabwork(user1.Id, "lab1", "desc1", new Points(100), "crt1", null);

            var labworksStorage = new List<Labwork>();
            labworksStorage.Add(basicLabwork);

            var subject1 = new ZachotSubject(user1.Id, "zasubj1", new Points(100), labworksStorage, null);
            var subject2 = new ExamSubject(user2.Id, "exsubj1", new Points(100), labworksStorage, null);
            var subject3 = new ExamSubject(user3.Id, "exsubj1", new Points(100), labworksStorage, null);

            subjectRepository.Add(subject1);

            Assert.Equal(subject1, subjectRepository.Get(subject1.Id));

            subjectRepository.Add(subject2);
            subjectRepository.Add(subject3);

            Assert.Equal(subject1, subjectRepository.Get(subject1.Id));
            Assert.Equal(subject2, subjectRepository.Get(subject2.Id));
            Assert.Equal(subject3, subjectRepository.Get(subject3.Id));

            Assert.Null(subjectRepository.Get(Guid.NewGuid()));

            Assert.True(subjectRepository.Remove(subject2.Id));

            Assert.Equal(subject1, subjectRepository.Get(subject1.Id));
            Assert.Null(subjectRepository.Get(subject2.Id));
            Assert.Equal(subject3, subjectRepository.Get(subject3.Id));
        }

        [Fact]
        public void LecturesRepository_AllFunctionsTest_AddsFindsRemovesCorrespondingly()
        {
            var lectureRepository = new BasicRepository<Lecture>();

            var user1 = new BasicUser("1");
            var user2 = new BasicUser("2");
            var user3 = new BasicUser("3");

            var basicLecture = new BasicLecture(user1.Id, "lec1", "desc1", "content1", null);
            var basicLecture2 = new BasicLecture(user2.Id, "lec1", "desc1", "content1", null);
            var basicLecture3 = new BasicLecture(user3.Id, "lec1", "desc1", "content1", null);

            lectureRepository.Add(basicLecture);

            Assert.Equal(basicLecture, lectureRepository.Get(basicLecture.Id));

            lectureRepository.Add(basicLecture2);
            lectureRepository.Add(basicLecture3);

            Assert.Equal(basicLecture, lectureRepository.Get(basicLecture.Id));
            Assert.Equal(basicLecture2, lectureRepository.Get(basicLecture2.Id));
            Assert.Equal(basicLecture3, lectureRepository.Get(basicLecture3.Id));

            Assert.Null(lectureRepository.Get(Guid.NewGuid()));

            Assert.True(lectureRepository.Remove(basicLecture2.Id));

            Assert.Equal(basicLecture, lectureRepository.Get(basicLecture.Id));
            Assert.Null(lectureRepository.Get(basicLecture2.Id));
            Assert.Equal(basicLecture3, lectureRepository.Get(basicLecture3.Id));
        }

        [Fact]
        public void LabworkRepository_AllFunctionsTest_AddsFindsRemovesCorrespondingly()
        {
            var labworkRepository = new BasicRepository<Labwork>();

            var user1 = new BasicUser("1");
            var user2 = new BasicUser("2");
            var user3 = new BasicUser("3");

            var basicLabwork = new BasicLabwork(user1.Id, "lec1", "desc1", new Points(10), "content1", null);
            var basicLabwork2 = new BasicLabwork(user1.Id, "lec1", "desc1", new Points(10), "content1", null);
            var basicLabwork3 = new BasicLabwork(user1.Id, "lec1", "desc1", new Points(10), "content1", null);

            labworkRepository.Add(basicLabwork);

            Assert.Equal(basicLabwork, labworkRepository.Get(basicLabwork.Id));

            labworkRepository.Add(basicLabwork2);
            labworkRepository.Add(basicLabwork3);

            Assert.Equal(basicLabwork, labworkRepository.Get(basicLabwork.Id));
            Assert.Equal(basicLabwork2, labworkRepository.Get(basicLabwork2.Id));
            Assert.Equal(basicLabwork3, labworkRepository.Get(basicLabwork3.Id));

            Assert.Null(labworkRepository.Get(Guid.NewGuid()));

            Assert.True(labworkRepository.Remove(basicLabwork2.Id));

            Assert.Equal(basicLabwork, labworkRepository.Get(basicLabwork.Id));
            Assert.Null(labworkRepository.Get(basicLabwork2.Id));
            Assert.Equal(basicLabwork3, labworkRepository.Get(basicLabwork3.Id));
        }

        [Fact]
        public void CourseRepository_AllFunctionsTest_AddsFindsRemovesCorrespondingly()
        {
            var courseRepository = new BasicRepository<Course>();

            var user1 = new BasicUser("1");
            var user2 = new BasicUser("2");
            var user3 = new BasicUser("3");

            var basicCourse = new BasicCourse("course1", user1.Id);
            var basicCourse2 = new BasicCourse("course2", user2.Id);
            var basicCourse3 = new BasicCourse("course3", user3.Id);

            courseRepository.Add(basicCourse);

            Assert.Equal(basicCourse, courseRepository.Get(basicCourse.Id));

            courseRepository.Add(basicCourse2);
            courseRepository.Add(basicCourse3);

            Assert.Equal(basicCourse, courseRepository.Get(basicCourse.Id));
            Assert.Equal(basicCourse2, courseRepository.Get(basicCourse2.Id));
            Assert.Equal(basicCourse3, courseRepository.Get(basicCourse3.Id));

            Assert.Null(courseRepository.Get(Guid.NewGuid()));

            Assert.True(courseRepository.Remove(basicCourse2.Id));

            Assert.Equal(basicCourse, courseRepository.Get(basicCourse.Id));
            Assert.Null(courseRepository.Get(basicCourse2.Id));
            Assert.Equal(basicCourse3, courseRepository.Get(basicCourse3.Id));
        }
    }
}