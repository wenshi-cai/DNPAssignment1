using RepositoryContracts;
using CLI.UI.ManageUsers;
using CLI.UI.ManagePosts;

namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository userRepository;
    private readonly ICommentRepository commentRepository;
    private readonly IPostRepository postRepository;

    public CliApp(
        IUserRepository userRepository,
        ICommentRepository commentRepository,
        IPostRepository postRepository)
    {
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
    }

    public async Task StartAsync()
    {
        Console.WriteLine("Starting CLI application...");

        bool running = true;

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("=== Main Menu ===");
            Console.WriteLine("1. Create new user");
            Console.WriteLine("2. Create new post");
            Console.WriteLine("3. View posts overview");
            Console.WriteLine("4. View specific post");
            Console.WriteLine("5. Add comment to post");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    CreateUserView createUserView =
                        new CreateUserView(userRepository);

                    await createUserView.StartAsync();
                    break;

                case "2":
                    CreatePostView createPostView =
                        new CreatePostView(postRepository);

                    await createPostView.StartAsync();
                    break;

                case "3":
                    ListPostsView listPostsView =
                        new ListPostsView(postRepository);

                    await listPostsView.StartAsync();
                    break;

                case "4":
                    SinglePostView singlePostView =
                        new SinglePostView(
                            postRepository,
                            commentRepository);

                    await singlePostView.StartAsync();
                    break;

                case "5":
                    AddCommentView addCommentView =
                        new AddCommentView(
                            commentRepository,
                            userRepository,
                            postRepository);

                    await addCommentView.StartAsync();
                    break;

                case "0":
                    running = false;
                    Console.WriteLine("Exiting application...");
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}