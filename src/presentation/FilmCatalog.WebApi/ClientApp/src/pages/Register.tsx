import { useForm } from "@mantine/form";
import {
  TextInput,
  PasswordInput,
  Text,
  Paper,
  Group,
  PaperProps,
  Button,
  Divider,
  Anchor,
  Stack,
  Container,
} from "@mantine/core";
import { Link } from "react-router-dom";
import { login } from "../services/userService";
import { extractErrors } from "../helpers/form";
import { useSignup } from "../api/identity";

export default function Register(props: PaperProps) {
  const form = useForm({
    initialValues: {
      email: "",
      username: "",
      password: "",
      passwordAgain: "",
    },

    validate: {
      email: (val: string) => (/^\S+@\S+$/.test(val) ? null : "Invalid email"),
      password: (val: string) =>
        val.length < 4 ? "Password should include at least 4 characters" : null,
    },
  });

  const mutationSignup = useSignup();

  const onSignup = async ({
    email,
    username,
    password,
    passwordAgain,
  }: typeof form.values) => {
    try {
      const { data } = await mutationSignup.mutateAsync({
        email,
        username,
        password,
        passwordAgain,
      });

      login({
        id: data.id,
        token: data.token,
        email: data.email,
        username: data.username,
        role: data.role,
      });
    } catch (e) {
      form.setErrors(extractErrors(e));
    }
  };

  return (
    <Container size={420} my={40}>
      <Paper radius="md" p="xl" withBorder {...props}>
        <Text size="lg" weight={500} align="center">
          Welcome to F-Catalog, register with
        </Text>

        <Divider my="lg" />

        <form onSubmit={form.onSubmit(onSignup)}>
          <Stack>
            <TextInput
              label="Name"
              placeholder="Your name"
              required
              {...form.getInputProps("username")}
              radius="md"
            />
            <TextInput
              label="Email"
              placeholder="email@example.com"
              required
              {...form.getInputProps("email")}
              radius="md"
            />
            <PasswordInput
              label="Password"
              placeholder="Your password"
              required
              {...form.getInputProps("password")}
              radius="md"
            />
            <PasswordInput
              label="Repeat Password"
              placeholder="Repeat password"
              required
              {...form.getInputProps("passwordAgain")}
              radius="md"
            />
          </Stack>

          <Group position="apart" mt="xl">
            <Link to="/login">
              <Anchor component="button" type="button" color="dimmed" size="xs">
                Already have an account? Login
              </Anchor>
            </Link>
            <Button type="submit" className="bg-blue-500">
              Register
            </Button>
          </Group>
        </form>
      </Paper>
    </Container>
  );
}
