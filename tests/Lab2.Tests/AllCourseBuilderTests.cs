using Itmo.ObjectOrientedProgramming.Lab2.Courses;
using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.Repository;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.Users;
using Itmo.ObjectOrientedProgramming.Lab2.ValueTypes;
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

            var bl = new BasicLabwork(testUser1.Id, "lab1", "desc1", new Points(100), "rc1", null);

            bl.RatingCriteria.SetValue(testUser1.Id, "new value");

            Assert.Equal("new value", bl.RatingCriteria.GetValue(testUser1.Id));
        }

        [Fact]
        public void Labwork_SetByStrangerTest_ThrowsException()
        {
            var testUser1 = new BasicUser("1");
            var testUser2 = new BasicUser("2");

            var bl = new BasicLabwork(testUser1.Id, "lab1", "desc1", new Points(100), "rc1", null);

            Assert.ThrowsAny<Exception>(() => bl.RatingCriteria.SetValue(testUser2.Id, "smth evil"));
            Assert.Equal("rc1", bl.RatingCriteria.GetValue(testUser2.Id));
        }

        [Fact]
        public void Labwork_DeepCopyCloneTest_CopyIsDeep()
        {
            var testUser1 = new BasicUser("1");
            var testUser2 = new BasicUser("2");

            var bl = new BasicLabwork(testUser1.Id, "lab1", "desc1", new Points(100), "rc1", null);

            Labwork bl2 = bl.Clone();

            Assert.Equal("lab1", bl2.Name.GetValue(testUser1.Id));
            Assert.NotEqual(bl.Id, bl2.Id);
            Assert.Equal(bl.Id, bl2.SourceLabworkId);

            bl.Name.SetValue(testUser1.Id, "new value");

            Assert.Equal("new value", bl.Name.GetValue(testUser1.Id));
            Assert.Equal("lab1", bl2.Name.GetValue(testUser1.Id));
        }

        [Fact]
        public void Labwork_BuilderTest_BuildsSuccessfully()
        {
            var lb = new BasicLabworkBuilder();

            var testUser1 = new BasicUser("1");
            var testUser2 = new BasicUser("2");

            lb.Author = testUser1.Id;
            lb.Description = "bububu";
            lb.Name = testUser1.Name + " lab";

            Labwork buildedLab = lb.BuildLabwork();

            Assert.Equal("bububu", buildedLab.Description.GetValue(testUser1.Id));
            Assert.Equal(testUser1.Name + " lab", buildedLab.Name.GetValue(testUser1.Id));
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

            var bl = new BasicLecture(testUser1.Id, "lec1", "desc1", "content1", null);

            bl.Content.SetValue(testUser1.Id, "new value");

            Assert.Equal("new value", bl.Content.GetValue(testUser1.Id));
        }

        [Fact]
        public void Lecture_SetByStrangerTest_ThrowsException()
        {
            var testUser1 = new BasicUser("1");
            var testUser2 = new BasicUser("2");

            var bl = new BasicLecture(testUser1.Id, "lec1", "desc1", "content1", null);

            Assert.ThrowsAny<Exception>(() => bl.Content.SetValue(testUser2.Id, "smth evil"));
            Assert.Equal("content1", bl.Content.GetValue(testUser2.Id));
        }

        [Fact]
        public void Lecture_DeepCopyCloneTest_CopyIsDeep()
        {
            var testUser1 = new BasicUser("1");
            var testUser2 = new BasicUser("2");

            var bl = new BasicLecture(testUser1.Id, "lec1", "desc1", "content1", null);

            Lecture bl2 = bl.Clone();

            Assert.Equal("lec1", bl2.Name.GetValue(testUser1.Id));
            Assert.NotEqual(bl.Id, bl2.Id);
            Assert.Equal(bl.Id, bl2.SourceId);

            bl.Name.SetValue(testUser1.Id, "new value");

            Assert.Equal("new value", bl.Name.GetValue(testUser1.Id));
            Assert.Equal("lec1", bl2.Name.GetValue(testUser1.Id));
        }

        [Fact]
        public void Lecture_BuilderTest_BuildsSuccessfully()
        {
            var lb = new BasicLectureBuilder();

            var testUser1 = new BasicUser("1");
            var testUser2 = new BasicUser("2");

            lb.AuthorId = testUser1.Id;
            lb.Name = testUser1.Name + " lec";
            lb.Description = "bububu";
            lb.Content = "content";

            Lecture buildedLab = lb.BuildLecture();

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
                var sb = new ExamSubjectBuilder();

                sb.ExamPoints = new Points(100);

                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                sb.AuthorId = testUser1.Id;

                var l1 = new BasicLabwork(testUser1.Id, "name1", "desc1", new Points(77), "crt1", null);
                var l2 = new BasicLabwork(testUser1.Id, "name2", "desc2", new Points(33), "crt2", null);

                sb.LabworksStorage.Add(l1);
                sb.LabworksStorage.Add(l2);

                Assert.ThrowsAny<Exception>(() => sb.BuildSubject());
            }

            [Fact]
            public void ExamSubject_SetByAuthorTest_ChangesValue()
            {
                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                var lab1 = new BasicLabwork(testUser1.Id, "lab1", "desc1", new Points(77), "crt1", null);
                var labworksStorage = new List<Labwork>();
                labworksStorage.Add(lab1);
                var bl = new ExamSubject(testUser1.Id, "exsubj1", new Points(20), labworksStorage, null);

                bl.Name.SetValue(testUser1.Id, "new value");

                Assert.Equal("new value", bl.Name.GetValue(testUser1.Id));
            }

            [Fact]
            public void ExamSubject_SetByStrangerTest_ThrowsException()
            {
                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                var lab1 = new BasicLabwork(testUser1.Id, "lab1", "desc1", new Points(77), "crt1", null);
                var labworksStorage = new List<Labwork>();
                labworksStorage.Add(lab1);
                var bl = new ExamSubject(testUser1.Id, "exsubj1", new Points(20), labworksStorage, null);

                Assert.ThrowsAny<Exception>(() => bl.Name.SetValue(testUser2.Id, "smth evil"));
                Assert.Equal("exsubj1", bl.Name.GetValue(testUser2.Id));
            }

            [Fact]
            public void ExamSubject_DeepCopyCloneTest_CopyIsDeep()
            {
                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                var lab1 = new BasicLabwork(testUser1.Id, "lab1", "desc1", new Points(77), "crt1", null);
                var labworksStorage = new List<Labwork>();
                labworksStorage.Add(lab1);
                var bl = new ExamSubject(testUser1.Id, "exsubj1", new Points(20), labworksStorage, null);

                var bl2 = bl.Clone() as ExamSubject;
                Assert.NotNull(bl2);
                Assert.IsType<ExamSubject>(bl2);

                Assert.Equal("exsubj1", bl2.Name.GetValue(testUser1.Id));
                Assert.NotEqual(bl.Id, bl2.Id);
                Assert.Equal(bl.Id, bl2.SourceId);

                bl.Name.SetValue(testUser1.Id, "new value");

                Assert.Equal("new value", bl.Name.GetValue(testUser1.Id));
                Assert.Equal("exsubj1", bl2.Name.GetValue(testUser1.Id));
            }

            [Fact]
            public void ExamSubject_BuildTest_BuildsSuccessfully()
            {
                var sb = new ExamSubjectBuilder();

                sb.ExamPoints = new Points(20);

                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                sb.AuthorId = testUser1.Id;
                sb.Name = "name";

                var l1 = new BasicLabwork(testUser1.Id, "name1", "desc1", new Points(60), "crt1", null);
                var l2 = new BasicLabwork(testUser1.Id, "name2", "desc2", new Points(20), "crt2", null);

                sb.LabworksStorage.Add(l1);
                sb.LabworksStorage.Add(l2);

                Subject ns = sb.BuildSubject();

                Assert.Equal(testUser1.Id, ns.AuthorId);
                Assert.Equal("name", ns.Name.GetValue(testUser2.Id));
            }

            [Fact]
            public void ExamSubject_BuildCloneTest_ClonesSuccessfully()
            {
                var sb = new ExamSubjectBuilder();

                sb.ExamPoints = new Points(20);

                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                sb.AuthorId = testUser1.Id;
                sb.Name = "name";

                var l1 = new BasicLabwork(testUser1.Id, "name1", "desc1", new Points(60), "crt1", null);
                var l2 = new BasicLabwork(testUser1.Id, "name2", "desc2", new Points(20), "crt2", null);

                sb.LabworksStorage.Add(l1);
                sb.LabworksStorage.Add(l2);

                Subject ns = sb.BuildSubject();

                Assert.Equal(testUser1.Id, ns.AuthorId);
                Assert.Equal("name", ns.Name.GetValue(testUser2.Id));

                Subject ns2 = ns.Clone();

                Assert.Equal(ns.Id, ns2.SourceId);
                Assert.Equal(ns.Name.GetValue(testUser1.Id), ns2.Name.GetValue(testUser2.Id));

                ns.Name.SetValue(testUser1.Id, "new name");

                Assert.NotEqual(ns.Name.GetValue(testUser1.Id), ns2.Name.GetValue(testUser2.Id));
            }
        }

        public class ZachotSubjectTests
        {
            [Fact]
            public void ZachotSubject_InvalidPointsTest_ThrowsException()
            {
                var sb = new ZachotSubjectBuilder();

                sb.ZachotPoints = new Points(100);

                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                sb.AuthorId = testUser1.Id;

                var l1 = new BasicLabwork(testUser1.Id, "name1", "desc1", new Points(77), "crt1", null);
                var l2 = new BasicLabwork(testUser1.Id, "name2", "desc2", new Points(33), "crt2", null);

                sb.LabworksStorage.Add(l1);
                sb.LabworksStorage.Add(l2);

                Assert.ThrowsAny<Exception>(() => sb.BuildSubject());
            }

            [Fact]
            public void ZachotSubject_SetByAuthorTest_ChangesValue()
            {
                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                var lab1 = new BasicLabwork(testUser1.Id, "lab1", "desc1", new Points(77), "crt1", null);
                var labworksStorage = new List<Labwork>();
                labworksStorage.Add(lab1);
                var bl = new ZachotSubject(testUser1.Id, "exsubj1", new Points(20), labworksStorage, null);

                bl.Name.SetValue(testUser1.Id, "new value");

                Assert.Equal("new value", bl.Name.GetValue(testUser1.Id));
            }

            [Fact]
            public void ZachotSubject_SetByStrangerTest_ThrowsException()
            {
                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                var lab1 = new BasicLabwork(testUser1.Id, "lab1", "desc1", new Points(77), "crt1", null);
                var labworksStorage = new List<Labwork>();
                labworksStorage.Add(lab1);
                var bl = new ZachotSubject(testUser1.Id, "exsubj1", new Points(20), labworksStorage, null);

                Assert.ThrowsAny<Exception>(() => bl.Name.SetValue(testUser2.Id, "smth evil"));
                Assert.Equal("exsubj1", bl.Name.GetValue(testUser2.Id));
            }

            [Fact]
            public void ZachotSubject_DeepCopyCloneTest_CopyIsDeep()
            {
                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                var lab1 = new BasicLabwork(testUser1.Id, "lab1", "desc1", new Points(77), "crt1", null);
                var labworksStorage = new List<Labwork>();
                labworksStorage.Add(lab1);
                var bl = new ZachotSubject(testUser1.Id, "exsubj1", new Points(20), labworksStorage, null);

                var bl2 = bl.Clone() as ZachotSubject;
                Assert.NotNull(bl2);
                Assert.IsType<ZachotSubject>(bl2);

                Assert.Equal("exsubj1", bl2.Name.GetValue(testUser1.Id));
                Assert.NotEqual(bl.Id, bl2.Id);
                Assert.Equal(bl.Id, bl2.SourceId);

                bl.Name.SetValue(testUser1.Id, "new value");

                Assert.Equal("new value", bl.Name.GetValue(testUser1.Id));
                Assert.Equal("exsubj1", bl2.Name.GetValue(testUser1.Id));
            }

            [Fact]
            public void ZachotSubject_BuildTest_BuildsSuccessfully()
            {
                var sb = new ZachotSubjectBuilder();

                sb.ZachotPoints = new Points(60);

                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                sb.AuthorId = testUser1.Id;
                sb.Name = "name";

                var l1 = new BasicLabwork(testUser1.Id, "name1", "desc1", new Points(70), "crt1", null);
                var l2 = new BasicLabwork(testUser1.Id, "name2", "desc2", new Points(30), "crt2", null);

                sb.LabworksStorage.Add(l1);
                sb.LabworksStorage.Add(l2);

                Subject ns = sb.BuildSubject();

                Assert.Equal(testUser1.Id, ns.AuthorId);
                Assert.Equal("name", ns.Name.GetValue(testUser2.Id));
            }

            [Fact]
            public void ZachotSubject_BuildCloneTest_ClonesSuccessfully()
            {
                var sb = new ZachotSubjectBuilder();

                sb.ZachotPoints = new Points(60);

                var testUser1 = new BasicUser("1");
                var testUser2 = new BasicUser("2");

                sb.AuthorId = testUser1.Id;
                sb.Name = "name";

                var l1 = new BasicLabwork(testUser1.Id, "name1", "desc1", new Points(70), "crt1", null);
                var l2 = new BasicLabwork(testUser1.Id, "name2", "desc2", new Points(30), "crt2", null);

                sb.LabworksStorage.Add(l1);
                sb.LabworksStorage.Add(l2);

                Subject ns = sb.BuildSubject();

                Assert.Equal(testUser1.Id, ns.AuthorId);
                Assert.Equal("name", ns.Name.GetValue(testUser2.Id));

                Subject ns2 = ns.Clone();

                Assert.Equal(ns.Id, ns2.SourceId);
                Assert.Equal(ns.Name.GetValue(testUser1.Id), ns2.Name.GetValue(testUser2.Id));

                ns.Name.SetValue(testUser1.Id, "new name");

                Assert.NotEqual(ns.Name.GetValue(testUser1.Id), ns2.Name.GetValue(testUser2.Id));
            }
        }
    }

    public class RepositoryTests
    {
        [Fact]
        public void UserRepository_AllFunctionsTest_AddsFindsRemovesCorrespondingly()
        {
            var userrepo = new BasicRepository<User>();

            var user1 = new BasicUser("1");
            var user2 = new BasicUser("2");
            var user3 = new BasicUser("3");

            userrepo.Add(user1);

            Assert.Equal(user1, userrepo.Get(user1.Id));

            userrepo.Add(user2);
            userrepo.Add(user3);

            Assert.Equal(user1, userrepo.Get(user1.Id));
            Assert.Equal(user2, userrepo.Get(user2.Id));
            Assert.Equal(user3, userrepo.Get(user3.Id));

            Assert.Null(userrepo.Get(Guid.NewGuid()));

            Assert.True(userrepo.Remove(user2.Id));

            Assert.Equal(user1, userrepo.Get(user1.Id));
            Assert.Null(userrepo.Get(user2.Id));
            Assert.Equal(user3, userrepo.Get(user3.Id));
        }

        [Fact]
        public void SubjectRepository_AllFunctionsTest_AddsFindsRemovesCorrespondingly()
        {
            var subjrepo = new BasicRepository<Subject>();

            var user1 = new BasicUser("1");
            var user2 = new BasicUser("2");
            var user3 = new BasicUser("3");

            var lab1 = new BasicLabwork(user1.Id, "lab1", "desc1", new Points(100), "crt1", null);

            var labworksStorage = new List<Labwork>();
            labworksStorage.Add(lab1);

            var subj1 = new ZachotSubject(user1.Id, "zasubj1", new Points(100), labworksStorage, null);
            var subj2 = new ExamSubject(user2.Id, "exsubj1", new Points(100), labworksStorage, null);
            var subj3 = new ExamSubject(user3.Id, "exsubj1", new Points(100), labworksStorage, null);

            subjrepo.Add(subj1);

            Assert.Equal(subj1, subjrepo.Get(subj1.Id));

            subjrepo.Add(subj2);
            subjrepo.Add(subj3);

            Assert.Equal(subj1, subjrepo.Get(subj1.Id));
            Assert.Equal(subj2, subjrepo.Get(subj2.Id));
            Assert.Equal(subj3, subjrepo.Get(subj3.Id));

            Assert.Null(subjrepo.Get(Guid.NewGuid()));

            Assert.True(subjrepo.Remove(subj2.Id));

            Assert.Equal(subj1, subjrepo.Get(subj1.Id));
            Assert.Null(subjrepo.Get(subj2.Id));
            Assert.Equal(subj3, subjrepo.Get(subj3.Id));
        }

        [Fact]
        public void LecturesRepository_AllFunctionsTest_AddsFindsRemovesCorrespondingly()
        {
            var lecrepo = new BasicRepository<Lecture>();

            var user1 = new BasicUser("1");
            var user2 = new BasicUser("2");
            var user3 = new BasicUser("3");

            var bl = new BasicLecture(user1.Id, "lec1", "desc1", "content1", null);
            var bl2 = new BasicLecture(user2.Id, "lec1", "desc1", "content1", null);
            var bl3 = new BasicLecture(user3.Id, "lec1", "desc1", "content1", null);

            lecrepo.Add(bl);

            Assert.Equal(bl, lecrepo.Get(bl.Id));

            lecrepo.Add(bl2);
            lecrepo.Add(bl3);

            Assert.Equal(bl, lecrepo.Get(bl.Id));
            Assert.Equal(bl2, lecrepo.Get(bl2.Id));
            Assert.Equal(bl3, lecrepo.Get(bl3.Id));

            Assert.Null(lecrepo.Get(Guid.NewGuid()));

            Assert.True(lecrepo.Remove(bl2.Id));

            Assert.Equal(bl, lecrepo.Get(bl.Id));
            Assert.Null(lecrepo.Get(bl2.Id));
            Assert.Equal(bl3, lecrepo.Get(bl3.Id));
        }

        [Fact]
        public void LabworkRepository_AllFunctionsTest_AddsFindsRemovesCorrespondingly()
        {
            var labrepo = new BasicRepository<Labwork>();

            var user1 = new BasicUser("1");
            var user2 = new BasicUser("2");
            var user3 = new BasicUser("3");

            var bl = new BasicLabwork(user1.Id, "lec1", "desc1", new Points(10), "content1", null);
            var bl2 = new BasicLabwork(user1.Id, "lec1", "desc1", new Points(10), "content1", null);
            var bl3 = new BasicLabwork(user1.Id, "lec1", "desc1", new Points(10), "content1", null);

            labrepo.Add(bl);

            Assert.Equal(bl, labrepo.Get(bl.Id));

            labrepo.Add(bl2);
            labrepo.Add(bl3);

            Assert.Equal(bl, labrepo.Get(bl.Id));
            Assert.Equal(bl2, labrepo.Get(bl2.Id));
            Assert.Equal(bl3, labrepo.Get(bl3.Id));

            Assert.Null(labrepo.Get(Guid.NewGuid()));

            Assert.True(labrepo.Remove(bl2.Id));

            Assert.Equal(bl, labrepo.Get(bl.Id));
            Assert.Null(labrepo.Get(bl2.Id));
            Assert.Equal(bl3, labrepo.Get(bl3.Id));
        }

        [Fact]
        public void CourseRepository_AllFunctionsTest_AddsFindsRemovesCorrespondingly()
        {
            var courserepo = new BasicRepository<Course>();

            var user1 = new BasicUser("1");
            var user2 = new BasicUser("2");
            var user3 = new BasicUser("3");

            var bc = new BasicCourse("course1", user1.Id);
            var bc2 = new BasicCourse("course2", user2.Id);
            var bc3 = new BasicCourse("course3", user3.Id);

            courserepo.Add(bc);

            Assert.Equal(bc, courserepo.Get(bc.Id));

            courserepo.Add(bc2);
            courserepo.Add(bc3);

            Assert.Equal(bc, courserepo.Get(bc.Id));
            Assert.Equal(bc2, courserepo.Get(bc2.Id));
            Assert.Equal(bc3, courserepo.Get(bc3.Id));

            Assert.Null(courserepo.Get(Guid.NewGuid()));

            Assert.True(courserepo.Remove(bc2.Id));

            Assert.Equal(bc, courserepo.Get(bc.Id));
            Assert.Null(courserepo.Get(bc2.Id));
            Assert.Equal(bc3, courserepo.Get(bc3.Id));
        }
    }
}